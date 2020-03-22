using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsForever_App.Repository;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore;

namespace FriendsForever_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRecaptchaService recaptcha;
        private readonly IAccountRepository accountRepository;

        public AccountController(IRecaptchaService recaptcha, IAccountRepository accountRepository)
        {
            this.recaptcha = recaptcha;
            this.accountRepository = accountRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var captcha = await recaptcha.Validate(Request);
            if (captcha.success)
            {

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
        public IActionResult Register(RegisterViewModel model)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult BindCountry(string Prefix)
        {
            var liCountries = (from countryList in accountRepository.GetCountries().ToList()
                         where countryList.ToUpper().Contains(Prefix.ToUpper())
                         select new { countryList });
            return Json(liCountries);
        }
    }
}