using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class FriendsProfileViewModel
    {
        public FriendsProfileViewModel()
        {
            UserInterests = new List<string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Country { get; set; }
        public IEnumerable<string> UserInterests { get; set; }
        public IEnumerable<string> ImagesGallery { get; set; }
        public bool SentFlag { get; set; }
    }
}
