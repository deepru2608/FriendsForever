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
            AllPhotos = new List<PostImages>();
            AllPosts = new List<Post>();
            PostPhotoCount = new List<CountPostImages>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime Dob { get; set; }
        public string ProfilePhotoPath { get; set; }
        public IEnumerable<string> UserInterests { get; set; }
        public IEnumerable<Post> AllPosts { get; set; }
        public List<CountPostImages> PostPhotoCount { get; set; }
        public IEnumerable<PostImages> AllPhotos { get; set; }
        public string YourStory { get; set; }
    }

    public class CountPostImages
    {
        public string PostedId { get; set; }
        public int PostPhotoCount { get; set; }
    }
}
