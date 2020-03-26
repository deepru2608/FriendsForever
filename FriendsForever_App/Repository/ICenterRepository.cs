using FriendsForever_App.Models;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public interface ICenterRepository
    {
        Task<int> InsertLogIntoLoginAsync(LogForLogin model);

        Task<int> InsertUserInterest(UserInterest userInterest);

        Task<IEnumerable<string>> FindByIdUserInterestAsync(string userId);

        Task<string> FileUploadMethodAsync(IFormFile Photo, string PhotoPath);

        Task<int> InsertNewPostAsync(Post post);

        Task<int> InsertNewPostImagesAsync(PostImages postImages);

        Task<IEnumerable<Post>> FindPostByUserIdAsync(string UserId);

        Task<IEnumerable<PostImages>> FindPostImagesByPostIdAsync(string postedId);

        Task<int> CheckPostTableDataAsync(string UserId);
    }
}
