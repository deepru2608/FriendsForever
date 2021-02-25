using FriendsForever_App.Data;
using FriendsForever_App.Models;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FriendsForever_App.Repository
{
    public class CenterRepository : ICenterRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IConfiguration configuration;

        public CenterRepository(AppDbContext dbContext, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<string>> FindByIdUserInterestAsync(string userId)
        {
            return await dbContext.UserInterestMaster
                .Where(w => w.UserId == userId)
                .Select(s => s.InterestName)
                .ToListAsync();
        }

        public async Task<int> InsertLogIntoLoginAsync(LogForLogin model)
        {
            dbContext.LogTableForLogin.Add(model);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertUserInterest(UserInterest userInterest)
        {
            dbContext.UserInterestMaster.Add(userInterest);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<string> FileUploadMethodAsync(IFormFile Photo, string PhotoPath)
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string folderPath = Path.Combine(hostEnvironment.WebRootPath, PhotoPath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }
            }

            return uniqueFileName;
        }

        public async Task<string> FileUploadMethodMultipleFolderAsync(IFormFile Photo, List<string> PhotoPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
            if (Photo != null)
            {
                foreach (var path in PhotoPath)
                {
                    string folderPath = Path.Combine(hostEnvironment.WebRootPath, path);
                    string filePath = Path.Combine(folderPath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Photo.CopyToAsync(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }

        public async Task<int> InsertNewPostAsync(Post post)
        {
            dbContext.PostMaster.Add(post);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertNewPostImagesAsync(PostImages postImages)
        {
            dbContext.PostImagesMaster.Add(postImages);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> FindPostByUserIdAsync(List<string> UserIds)
        {
            var result = await dbContext.PostMaster.Where(w => UserIds.Contains(w.PostedOwnerUserId))
                .OrderByDescending(o => o.PostedTimeStamp).ToListAsync();
            return result;
        }

        public async Task<Post> FindPostByPostIdAsync(string postId)
        {
            var result = await dbContext.PostMaster.FirstOrDefaultAsync(w => w.PostedId.Contains(postId));
            return result;
        }

        public async Task<IEnumerable<PostImages>> FindPostImagesByPostIdAsync(string postedId)
        {
            return await dbContext.PostImagesMaster.Where(w => w.PostedId == postedId).ToListAsync();
        }

        public async Task<int> CheckPostTableDataAsync(string UserId)
        {
            var resultCount = await dbContext.PostMaster.Where(w => w.PostedOwnerUserId == UserId).CountAsync();
            return resultCount;
        }

        public IEnumerable<string> SearchNewFriends(string userId)
        {
            return dbContext.Users.Where(w => w.Id != userId).Select(e => e.FullName).ToList();
        }

        public async Task<IEnumerable<string>> FindFriendsProfilePhotosAsync(string userId)
        {
            var result = from post in await dbContext.PostMaster.ToListAsync()
                         join postImages in await dbContext.PostImagesMaster.ToListAsync() on post.PostedId equals postImages.PostedId
                         where post.PostedOwnerUserId == userId
                         select new
                         {
                             allImages = postImages.PostedPhoto
                         };
            List<string> li = new List<string>();
            foreach (var images in result)
            {
                li.Add(images.allImages);
            }
            return li;
        }

        public async Task<int> InsertFriendsReuqestDataAsync(Friends friends)
        {
            dbContext.FriendsMaster.Add(friends);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckFriendRequestSentOrNotAsync(string SenderId, string RequestId)
        {
            bool check = true;
            var result = await dbContext.FriendsMaster
                .Where(w => w.UserIdRequest == SenderId && w.UserIdResponse == RequestId && (w.Status == "Pending" || w.Status == "Accept"))
                .CountAsync();
            if (result > 0)
            {
                check = false;
            }
            return check;
        }

        public async Task<IEnumerable<ShowFriendsReuqestData>> GetPendingFriendsRequestAsync(string userId)
        {
            var result = await dbContext.FriendsMaster
                .Where(w => w.UserIdRequest == userId && w.Status == "Pending")
                .Select(s => s.UserIdResponse).ToListAsync();
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var item in result)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(f => f.Id == item);
                users.Add(user);
            }
            List<ShowFriendsReuqestData> model = new List<ShowFriendsReuqestData>();
            foreach (var friend in users)
            {
                model.Add(new ShowFriendsReuqestData
                {
                    ProfilePhotoPath = friend.ProfilePhotoPath,
                    SenderFullName = friend.FullName
                });
            }
            return model;
        }

        public async Task<IEnumerable<ShowFriendsReuqestData>> GetNewFriendsRequestAsync(string userId)
        {
            var result = await dbContext.FriendsMaster
                .Where(w => w.UserIdResponse == userId && w.Status == "Pending")
                .Select(s => s.UserIdRequest).ToListAsync();
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var item in result)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(f => f.Id == item);
                users.Add(user);
            }
            List<ShowFriendsReuqestData> model = new List<ShowFriendsReuqestData>();
            foreach (var friend in users)
            {
                model.Add(new ShowFriendsReuqestData
                {
                    ProfilePhotoPath = friend.ProfilePhotoPath,
                    SenderFullName = friend.FullName
                });
            }
            return model;
        }

        public async Task<int> AcceptRejectRequestUpdateAsync(Friends model)
        {
            var friend = await dbContext.FriendsMaster
                .Where(w => w.UserIdRequest == model.UserIdRequest && w.Status == "Pending")
                .FirstOrDefaultAsync();
            if (friend != null)
            {
                friend.Status = model.Status;
                friend.UserIdRequest = model.UserIdRequest;
                dbContext.FriendsMaster.Update(friend);
            }
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowAllFriend>> GetAllFriendsAsync(string userId)
        {
            var result = await dbContext.FriendsMaster
                .Where(w => w.UserIdRequest == userId && w.Status == "Accept")
                .Select(s => s.UserIdResponse).ToListAsync();
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var item in result)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(f => f.Id == item);
                users.Add(user);
            }
            List<ShowAllFriend> model = new List<ShowAllFriend>();
            foreach (var friend in users)
            {
                model.Add(new ShowAllFriend
                {
                    FriendFullName = friend.FullName,
                    ProfilePhotoPath = friend.ProfilePhotoPath
                });
            }
            return model;
        }

        public async Task<int> InsertPostLikesAsync(Likes model)
        {
            dbContext.LikesMaster.Add(model);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> CheckDuplicateLikesAsync(string UserId, string postId)
        {
            var result = await dbContext.LikesMaster.Where(w => w.LikeUserId == UserId && w.PostedId == postId).CountAsync();
            return result;
        }

        public async Task<int> UpdatePostMasterLikesAsync(string postId)
        {
            var post = await dbContext.PostMaster.FirstOrDefaultAsync(f => f.PostedId == postId);
            var likeCount = await dbContext.LikesMaster.Where(w => w.PostedId == postId).CountAsync();
            if (post != null)
            {
                post.LikesCount = likeCount;
                dbContext.Update(post).Property(p => p.Id).IsModified = false;
            }
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Friends>> GetFriendsListByUserIdAsync(string userId)
        {
            return await dbContext.FriendsMaster
                .Where(w => w.UserIdRequest == userId && w.Status.ToLower() == "accept").ToListAsync();
        }

        public async Task<int> InsertPostCommentsAsync(Comments model)
        {
            string Connstr = configuration.GetConnectionString("DefaultConnection");
            var insertModel = new
            {
                postId = model.PostedId,
                userId = model.CommentedUserId,
                comment = model.Comment
            };
            using (IDbConnection connection = new SqlConnection(Connstr))
            {
                var result = await connection.ExecuteAsync("SP_InsertComments @postId, @userId, @comment", insertModel);
                return result;
            }
        }

        public async Task<int> UpdatePostMasterCommentsAsync(string postId)
        {
            int result = 0;
            string Connstr = configuration.GetConnectionString("DefaultConnection");
            var post = await dbContext.PostMaster.FirstOrDefaultAsync(f => f.PostedId == postId);
            var commentCount = await dbContext.CommentsMaster.Where(w => w.PostedId == postId).CountAsync();
            if (post != null)
            {
                var updateModel = new
                {
                    commentCount = commentCount,
                    PostedId = postId
                };
                using (IDbConnection connection = new SqlConnection(Connstr))
                {
                    result = await connection.ExecuteAsync("Update PostMaster set CommentsCount = @commentCount where PostedId = @postId", updateModel);
                }
            }
            return result;
        }

        public async Task<IEnumerable<Comments>> GetCommentsListByPostIdAsync(string postId)
        {
            return await dbContext.CommentsMaster.Where(w => w.PostedId == postId).ToListAsync();
        }
    }
}
