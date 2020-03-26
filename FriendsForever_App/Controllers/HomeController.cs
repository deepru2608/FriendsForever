using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FriendsForever_App.Models;
using FriendsForever_App.Repository;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FriendsForever_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountRepository accountRepository;
        private readonly ICenterRepository centerRepository;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(UserManager<ApplicationUser> userManager, IAccountRepository accountRepository, ICenterRepository centerRepository,
            IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.accountRepository = accountRepository;
            this.centerRepository = centerRepository;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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
                int count = await centerRepository.CheckPostTableDataAsync(user.Id);
                if (count > 0)
                {
                    model.AllPosts = await centerRepository.FindPostByUserIdAsync(user.Id);
                    List<PostImages> postImagesList = new List<PostImages>();
                    foreach (var post in model.AllPosts)
                    {
                        var result = await centerRepository.FindPostImagesByPostIdAsync(post.PostedId);
                        model.PostPhotoCount.Add(new CountPostImages
                        {
                            PostedId = post.PostedId,
                            PostPhotoCount = result.Count()
                        });
                        foreach (var postImages in result)
                        {
                            PostImages postImages1 = new PostImages
                            {
                                Id = postImages.Id,
                                PostedId = postImages.PostedId,
                                PostedPhoto = postImages.PostedPhoto
                            };
                            postImagesList.Add(postImages1);
                        }
                    }
                    model.AllPhotos = postImagesList;
                }
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
            return RedirectToAction("Index");
        }
    }
}