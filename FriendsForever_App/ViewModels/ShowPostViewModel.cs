using FriendsForever_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class ShowPostViewModel
    {
        public ShowPostViewModel()
        {
            AllPhotos = new List<PostImages>();
            AllPosts = new List<Post>();
        }

        public IEnumerable<Post> AllPosts { get; set; }
        public IEnumerable<PostImages> AllPhotos { get; set; }
    }
}
