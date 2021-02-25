using FriendsForever_App.Models;
using FriendsForever_App.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<string> GetCountries();

        ApplicationUser IsMobileInUse(string Mobile);

        Task<int> UpdateUserProfile(ApplicationUser user);

        Task<ApplicationUser> GetUserByNameAsync(string Name);

        Task<ApplicationUser> GetUserByUserIdAsync(string userId);
    }
}
