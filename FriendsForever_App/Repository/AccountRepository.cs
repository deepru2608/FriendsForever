using FriendsForever_App.Data;
using FriendsForever_App.Models;
using FriendsForever_App.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<string> GetCountries()
        {
            var liCountries = dbContext.CountryMaster.Select(e => e.CountryName).ToList();
            return liCountries;
        }

        public ApplicationUser IsMobileInUse(string Mobile)
        {
            return dbContext.Users.FirstOrDefault(e => e.PhoneNumber.Contains(Mobile)); ;
        }

        public async Task<int> UpdateUserProfile(ApplicationUser updatedUser)
        {
            ApplicationUser user = await dbContext.Users.FirstOrDefaultAsync(f => f.Id == updatedUser.Id);
            int result = 0;
            if (user != null)
            {
                if (dbContext.ChangeTracker.HasChanges())
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.FullName = $"{updatedUser.FirstName} {updatedUser.LastName}";
                    user.ProfilePhotoPath = updatedUser.ProfilePhotoPath;
                    user.Id = updatedUser.Id;
                    dbContext.Update(user);
                    return await dbContext.SaveChangesAsync();
                }
                else
                {
                    return 1;
                }
            }
            return result;
        }
    }
}
