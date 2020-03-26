using FriendsForever_App.Data;
using FriendsForever_App.Models;
using FriendsForever_App.Security;
using FriendsForever_App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public class CenterRepository : ICenterRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public CenterRepository(AppDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
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

        public async Task<IEnumerable<Post>> FindPostByUserIdAsync(string UserId)
        {
            var result = await dbContext.PostMaster.Where(w => w.PostedOwnerUserId == UserId).OrderByDescending(o => o.PostedTimeStamp).ToListAsync();
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
    }
}
