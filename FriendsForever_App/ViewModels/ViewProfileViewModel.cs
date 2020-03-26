using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class ViewProfileViewModel
    {
        public ViewProfileViewModel()
        {
            UserInterests = new List<string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string ProfilePhotoPath { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public IEnumerable<string> UserInterests { get; set; }
        public string Interest { get; set; }
        public bool UpdationFlag { get; set; }
    }
}
