using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsForever_App.Helpers;
using FriendsForever_App.Models;
using FriendsForever_App.Repository;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using reCAPTCHA.AspNetCore;

namespace FriendsForever_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRecaptchaService recaptcha;
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICenterRepository centerRepository;

        public AccountController(IRecaptchaService recaptcha, IAccountRepository accountRepository, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor,
            ICenterRepository centerRepository)
        {
            this.recaptcha = recaptcha;
            this.accountRepository = accountRepository;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.centerRepository = centerRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var captcha = await recaptcha.Validate(Request);
            if (captcha.success)
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(model.Username);
                    if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, model.Password)))
                    {
                        ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                        return View(model);
                    }

                    var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);
                    var loginLog = new LogForLogin
                    {
                        UserName = user.UserName,
                        ApplicationUser = user,
                        Ip_Address = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                        TimeStamp = DateTime.Now,
                    };
                    if (result.Succeeded)
                    {
                        loginLog.StatusFlag = 1;
                        await centerRepository.InsertLogIntoLoginAsync(loginLog);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        loginLog.StatusFlag = 0;
                        await centerRepository.InsertLogIntoLoginAsync(loginLog);
                    }

                    if (result.IsLockedOut)
                    {
                        ViewBag.Title = "Account Locked";
                        ViewBag.Message = "Your account is locked, please try again after sometime or you may reset your password.";
                        return View("AccountLocked");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }

            ModelState.AddModelError("", "There was an error validating recatpcha. Please try again!");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var captcha = await recaptcha.Validate(Request);
            if (captcha.success)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.MobileNo,
                        Gender = model.Gender,
                        Country = model.Country,
                        Dob = model.Dob
                    };

                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        string FullName = $"{model.FirstName} {model.LastName}";
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                        var body = $"Name: {FullName}<br />Email: {model.Email}<br />Mobile Number: {model.MobileNo}<br />" +
                            $"Confirmation Link: {confirmationLink}";
                        var mailHelper = new MailHelper(configuration);
                        var senderEmail = configuration.GetSection("Gmail:Username");
                        if (mailHelper.Send(senderEmail.Value, model.Email, "Confirmation Your Email", body))
                        {
                            ViewBag.ErrorTitle = "Registration successful";
                            ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                                    "email, by clicking on the confirmation link we have emailed you";
                            return View("NotFound");
                        }

                        ModelState.AddModelError(string.Empty, "Email not sent");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            ModelState.AddModelError("", "There was an error validating recatpcha. Please try again!");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult BindCountry(string Prefix = "")
        {
            var liCountries = (from countryList in accountRepository.GetCountries().ToList()
                               where countryList.ToUpper().Contains(Prefix.ToUpper())
                               select new { countryList });
            return Json(liCountries);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User Id {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorMessage = $"The User Id {userId} is invalid";
            return View("NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use!");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public IActionResult IsMobileInUse(string MobileNo)
        {
            var user = accountRepository.IsMobileInUse(MobileNo);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Mobile {MobileNo} is already in use!");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccountLocked() => View();
    }
}