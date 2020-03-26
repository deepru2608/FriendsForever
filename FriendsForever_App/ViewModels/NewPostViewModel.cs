using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class NewPostViewModel
    {
        public List<IFormFile> PostedPhotos { get; set; }
        public string PostedContent { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string FullName { get; set; }
    }
}
