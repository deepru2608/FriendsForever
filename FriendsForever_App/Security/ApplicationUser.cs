using Microsoft.AspNetCore.Identity;

namespace FriendsForever_App.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Doe { get; set; }
        public string Gender { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Country { get; set; }
    }
}