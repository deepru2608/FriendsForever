using FriendsForever_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class UserDashboardViewModel
    {
        public UserDashboardViewModel()
        {
            UserInterests = new List<string>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime Dob { get; set; }
        public string ProfilePhotoPath { get; set; }
        public IEnumerable<string> UserInterests { get; set; }
        public string YourStory { get; set; }
        public string SearchFriend { get; set; }
        public string userId { get; set; }
    }
}
