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

        Task<string> FileUploadMethodMultipleFolderAsync(IFormFile Photo, List<string> PhotoPath);

        Task<int> InsertNewPostAsync(Post post);

        Task<int> InsertNewPostImagesAsync(PostImages postImages);

        Task<IEnumerable<Post>> FindPostByUserIdAsync(List<string> UserIds);

        Task<Post> FindPostByPostIdAsync(string postId);

        Task<IEnumerable<PostImages>> FindPostImagesByPostIdAsync(string postedId);

        Task<int> CheckPostTableDataAsync(string UserId);

        IEnumerable<string> SearchNewFriends(string userId);

        Task<IEnumerable<string>> FindFriendsProfilePhotosAsync(string userId);

        Task<int> InsertFriendsReuqestDataAsync(Friends friends);

        Task<bool> CheckFriendRequestSentOrNotAsync(string SenderId, string RequestId);

        Task<IEnumerable<ShowFriendsReuqestData>> GetPendingFriendsRequestAsync(string userId);

        Task<IEnumerable<ShowFriendsReuqestData>> GetNewFriendsRequestAsync(string userId);

        Task<int> AcceptRejectRequestUpdateAsync(Friends model);

        Task<IEnumerable<ShowAllFriend>> GetAllFriendsAsync(string userId);

        Task<int> InsertPostLikesAsync(Likes model);

        Task<int> CheckDuplicateLikesAsync(string UserId, string postId);

        Task<int> UpdatePostMasterLikesAsync(string postId);

        Task<IEnumerable<Friends>> GetFriendsListByUserIdAsync(string userId);

        Task<int> InsertPostCommentsAsync(Comments model);

        Task<int> UpdatePostMasterCommentsAsync(string postId);

        Task<IEnumerable<Comments>> GetCommentsListByPostIdAsync(string postId);
    }
}
