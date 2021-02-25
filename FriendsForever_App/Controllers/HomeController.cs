using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FriendsForever_App.Models;
using FriendsForever_App.Repository;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendsForever_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountRepository accountRepository;
        private readonly ICenterRepository centerRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IDataProtector dataProtector;

        public HomeController(UserManager<ApplicationUser> userManager, IAccountRepository accountRepository, ICenterRepository centerRepository,
            IWebHostEnvironment hostEnvironment, IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.userManager = userManager;
            this.accountRepository = accountRepository;
            this.centerRepository = centerRepository;
            this.hostEnvironment = hostEnvironment;
            dataProtector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.RouteValue);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string successMessage = null)
        {
            if (!string.IsNullOrEmpty(successMessage))
            {
                ViewBag.SuccessMessage = dataProtector.Unprotect(successMessage);
            }

            var errorValue = HttpContext.Session.GetString("ErrorMessage");
            if (!string.IsNullOrEmpty(errorValue))
            {
                ViewBag.ErrorMessage = errorValue;
                HttpContext.Session.Remove("ErrorMessage");
            }

            var user = await userManager.GetUserAsync(User);
            UserDashboardViewModel model = new UserDashboardViewModel();
            if (user != null)
            {
                model.Name = user.FullName;
                model.Dob = user.Dob;
                model.Country = user.Country;
                model.Email = user.Email;
                model.ProfilePhotoPath = user.ProfilePhotoPath;
                IEnumerable<string> interests = await centerRepository.FindByIdUserInterestAsync(user.Id);
                model.UserInterests = interests;
                model.userId = user.Id;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var user = await userManager.GetUserAsync(User);
            ViewProfileViewModel model = new ViewProfileViewModel();
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Dob = user.Dob;
                model.Country = user.Country;
                model.Email = user.Email;
                model.ProfilePhotoPath = user.ProfilePhotoPath;
                model.MobileNo = user.PhoneNumber;
                model.Gender = user.Gender;
                IEnumerable<string> interests = await centerRepository.FindByIdUserInterestAsync(user.Id);
                model.UserInterests = interests;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ViewProfile(ViewProfileViewModel model)
        {
            if (model.UpdationFlag)
            {
                var user = await userManager.GetUserAsync(User);
                string Interest = model.Interest;
                string[] userInterests;
                if (model.Interest != null)
                {
                    if (model.Interest.Contains(","))
                    {
                        userInterests = model.Interest.Split(',');
                        for (int i = 0; i < userInterests.Length; i++)
                        {
                            UserInterest userThings = new UserInterest
                            {
                                UserId = user.Id,
                                InterestName = userInterests[i],
                                Added_Time_Stamp = DateTime.Now
                            };
                            await centerRepository.InsertUserInterest(userThings);
                        }
                    }
                    else
                    {
                        UserInterest userThings = new UserInterest
                        {
                            UserId = user.Id,
                            InterestName = Interest,
                            Added_Time_Stamp = DateTime.Now
                        };
                        await centerRepository.InsertUserInterest(userThings);
                    }
                }
                string uniqueProfilePhotoName = user.ProfilePhotoPath;
                if (model.ProfilePhoto != null)
                {
                    uniqueProfilePhotoName = await centerRepository.FileUploadMethodAsync(model.ProfilePhoto, "Images/ProfileImage");
                    string postId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                    string uniqueFileName = null;
                    string uploadFolder = Path.Combine("Images", "PostImages");
                    uniqueFileName = await centerRepository.FileUploadMethodAsync(model.ProfilePhoto, uploadFolder);

                    PostImages postImages = new PostImages
                    {
                        PostedId = postId,
                        PostedPhoto = uniqueFileName
                    };
                    var returnResult = await centerRepository.InsertNewPostImagesAsync(postImages);
                    if (returnResult > 0)
                    {
                        Post post = new Post
                        {
                            PostedId = postId,
                            PostedOwnerName = user.FullName,
                            PostedOwnerPhoto = uniqueProfilePhotoName,
                            PostedOwnerUserId = user.Id,
                            PostedContent = null,
                            PhotoAttached = "Attached",
                            PostedTimeStamp = DateTime.Now
                        };
                        await centerRepository.InsertNewPostAsync(post);
                    }
                }

                ApplicationUser updatedUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = $"{model.FirstName} {model.LastName}",
                    ProfilePhotoPath = uniqueProfilePhotoName,
                    Id = user.Id
                };
                var result = await accountRepository.UpdateUserProfile(updatedUser);
                if (result > 0)
                {
                    return RedirectToAction("ViewProfile");
                }
            }
            else
            {
                return RedirectToAction("ViewProfile");
            }
            return View("ViewProfile");
        }

        [HttpGet]
        public async Task<IActionResult> NewPost()
        {
            var user = await userManager.GetUserAsync(User);
            NewPostViewModel model = new NewPostViewModel();
            if (user != null)
            {
                model.FullName = user.FullName;
                model.ProfilePhotoPath = user.ProfilePhotoPath;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                {
                    string postId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                    if (model.PostedPhotos != null && model.PostedPhotos.Count > 0)
                    {
                        int count = 0;
                        string uniqueFileName = null;
                        foreach (IFormFile photo in model.PostedPhotos)
                        {
                            string uploadFolder = Path.Combine("Images", "PostImages");
                            uniqueFileName = await centerRepository.FileUploadMethodAsync(photo, uploadFolder);

                            PostImages postImages = new PostImages
                            {
                                PostedId = postId,
                                PostedPhoto = uniqueFileName
                            };
                            var returnResult = await centerRepository.InsertNewPostImagesAsync(postImages);
                            if (returnResult > 0)
                            {
                                count = count + returnResult;
                            }
                        }

                        if (count == model.PostedPhotos.Count())
                        {
                            Post post = new Post
                            {
                                PostedId = postId,
                                PostedOwnerName = user.FullName,
                                PostedOwnerPhoto = user.ProfilePhotoPath,
                                PostedOwnerUserId = user.Id,
                                PostedContent = model.PostedContent,
                                PhotoAttached = "Attached",
                                PostedTimeStamp = DateTime.Now
                            };
                            var result = await centerRepository.InsertNewPostAsync(post);
                            if (result > 0)
                            {
                                return RedirectToAction("Index");
                            }
                            return View(model);
                        }
                        else
                        {
                            foreach (IFormFile photo in model.PostedPhotos)
                            {
                                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "Images", "PostImages", photo.FileName);
                                FileInfo file = new FileInfo(uploadsFolder);
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }

                            ModelState.AddModelError(string.Empty, "We are facing some technical problem with post service, Please try again later!");
                            return View(model);
                        }
                    }
                    else
                    {
                        Post post = new Post
                        {
                            PostedId = postId,
                            PostedOwnerName = user.FullName,
                            PostedOwnerPhoto = user.ProfilePhotoPath,
                            PostedOwnerUserId = user.Id,
                            PostedContent = model.PostedContent,
                            PhotoAttached = "Not Attached",
                            PostedTimeStamp = DateTime.Now
                        };
                        var result = await centerRepository.InsertNewPostAsync(post);
                        if (result > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        return View(model);
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Please fill all fields!");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewStory(UserDashboardViewModel model)
        {
            if (model.YourStory != null)
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                {
                    string postId = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                    Post post = new Post
                    {
                        PostedId = postId,
                        PostedOwnerName = user.FullName,
                        PostedOwnerPhoto = user.ProfilePhotoPath,
                        PostedOwnerUserId = user.Id,
                        PostedContent = model.YourStory,
                        PhotoAttached = "Not Attached",
                        PostedTimeStamp = DateTime.Now
                    };
                    var result = await centerRepository.InsertNewPostAsync(post);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            HttpContext.Session.SetString("ErrorMessage", "Please fill this field!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> SearchNewFriends(string Prefix = "")
        {
            var user = await userManager.GetUserAsync(User);
            var liFriendsList = from friendsList in centerRepository.SearchNewFriends(user.Id).ToList()
                                where friendsList.ToUpper().Contains(Prefix.ToUpper())
                                select new { friendsList };
            return Json(liFriendsList);
        }


        [HttpGet]
        public async Task<IActionResult> FriendsViewProfile(string encName)
        {
            FriendsProfileViewModel viewModel = new FriendsProfileViewModel();
            string FullName = string.Empty;
            if (encName != null)
            {
                FullName = dataProtector.Unprotect(encName);
            }
            if (FullName != null)
            {
                var friendRequestUser = await accountRepository.GetUserByNameAsync(FullName);
                var user = await userManager.GetUserAsync(User);
                if (friendRequestUser != null && user != null)
                {
                    viewModel.FirstName = friendRequestUser.FirstName;
                    viewModel.LastName = friendRequestUser.LastName;
                    viewModel.Dob = friendRequestUser.Dob;
                    viewModel.Country = friendRequestUser.Country;
                    viewModel.Gender = friendRequestUser.Gender;
                    viewModel.ProfilePhotoPath = friendRequestUser.ProfilePhotoPath;
                    IEnumerable<string> interests = await centerRepository.FindByIdUserInterestAsync(friendRequestUser.Id);
                    viewModel.UserInterests = interests;
                    IEnumerable<string> allImages = await centerRepository.FindFriendsProfilePhotosAsync(friendRequestUser.Id);
                    viewModel.ImagesGallery = allImages;
                    viewModel.SentFlag = await centerRepository.CheckFriendRequestSentOrNotAsync(user.Id, friendRequestUser.Id);
                }
            }
            else
            {
                HttpContext.Session.SetString("ErrorMessage", "Please fill this field!");
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FriendsViewProfile(UserDashboardViewModel model)
        {
            FriendsProfileViewModel viewModel = new FriendsProfileViewModel();
            if (model.SearchFriend != null)
            {
                var friendRequestUser = await accountRepository.GetUserByNameAsync(model.SearchFriend);
                var user = await userManager.GetUserAsync(User);
                if (friendRequestUser != null && user != null)
                {
                    viewModel.FirstName = friendRequestUser.FirstName;
                    viewModel.LastName = friendRequestUser.LastName;
                    viewModel.Dob = friendRequestUser.Dob;
                    viewModel.Country = friendRequestUser.Country;
                    viewModel.Gender = friendRequestUser.Gender;
                    viewModel.ProfilePhotoPath = friendRequestUser.ProfilePhotoPath;
                    IEnumerable<string> interests = await centerRepository.FindByIdUserInterestAsync(friendRequestUser.Id);
                    viewModel.UserInterests = interests;
                    IEnumerable<string> allImages = await centerRepository.FindFriendsProfilePhotosAsync(friendRequestUser.Id);
                    viewModel.ImagesGallery = allImages;
                    viewModel.SentFlag = await centerRepository.CheckFriendRequestSentOrNotAsync(user.Id, friendRequestUser.Id);
                }
            }
            else
            {
                HttpContext.Session.SetString("ErrorMessage", "Please fill this field!");
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string name, UserDashboardViewModel model)
        {
            string RequestUser = string.Empty;
            if (model.SearchFriend != null)
            {
                RequestUser = model.SearchFriend;
            }
            else
            {
                RequestUser = name;
            }
            if (RequestUser != null)
            {
                var friendRequestUser = await accountRepository.GetUserByNameAsync(RequestUser);
                var friendSenderUser = await userManager.GetUserAsync(User);
                bool checkDuplicateRequest = await centerRepository.CheckFriendRequestSentOrNotAsync(friendSenderUser.Id, friendRequestUser.Id);
                if (checkDuplicateRequest)
                {
                    if (friendRequestUser != null && friendSenderUser != null)
                    {
                        Friends friends = new Friends
                        {
                            UserIdRequest = friendSenderUser.Id,
                            UserIdResponse = friendRequestUser.Id,
                            Time_Stamp = DateTime.Now,
                            UniqueId = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        };

                        var result = await centerRepository.InsertFriendsReuqestDataAsync(friends);
                        if (result > 0)
                        {
                            string message = dataProtector.Protect($"You have sent the request to {friendRequestUser.FullName} succesfully.");
                            return RedirectToAction(nameof(Index), new { successMessage = message });
                        }
                    }
                }
                else
                {
                    HttpContext.Session.SetString("ErrorMessage", "You have already sent the friend request, please wait for acceptance!");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                HttpContext.Session.SetString("ErrorMessage", "Please fill this field!");
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AcceptFriendRequest(string encName)
        {
            string FullName = string.Empty;
            if (encName != null)
            {
                FullName = dataProtector.Unprotect(encName);
            }
            var friendSenderUser = await accountRepository.GetUserByNameAsync(FullName);
            var friendRequestUser = await userManager.GetUserAsync(User);
            if (friendSenderUser != null)
            {
                Friends friend = new Friends
                {
                    Status = "Accept",
                    UserIdRequest = friendSenderUser.Id
                };
                var updateResult = await centerRepository.AcceptRejectRequestUpdateAsync(friend);
                if (updateResult > 0)
                {
                    Friends friends = new Friends
                    {
                        UserIdRequest = friendRequestUser.Id,
                        UserIdResponse = friendSenderUser.Id,
                        Time_Stamp = DateTime.Now,
                        UniqueId = Guid.NewGuid().ToString().GetHashCode().ToString("x"),
                        Status = "Accept"
                    };

                    var result = await centerRepository.InsertFriendsReuqestDataAsync(friends);
                    if (result > 0)
                    {
                        string message = dataProtector.Protect($"You have accepted {friendRequestUser.FullName} request succesfully.");
                        return RedirectToAction(nameof(Index), new { successMessage = message });
                    }
                    else
                    {
                        HttpContext.Session.SetString("ErrorMessage", "We are facing some technical issue, please try again later!");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RejectFriendRequest(string encName)
        {
            string FullName = string.Empty;
            if (encName != null)
            {
                FullName = dataProtector.Unprotect(encName);
            }
            var friendSenderUser = await accountRepository.GetUserByNameAsync(FullName);
            if (friendSenderUser != null)
            {
                Friends friend = new Friends
                {
                    Status = "Reject",
                    UserIdRequest = friendSenderUser.Id
                };
                var result = await centerRepository.AcceptRejectRequestUpdateAsync(friend);
                if (result > 0)
                {
                    string message = dataProtector.Protect($"You have rejected {friendSenderUser.FullName} request succesfully.");
                    return RedirectToAction(nameof(Index), new { successMessage = message });
                }
                else
                {
                    HttpContext.Session.SetString("ErrorMessage", "We are facing some technical issue, please try again later!");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CommentBox()
        {
            return View();
        }
    }
}