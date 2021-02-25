using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class ShowCommentsViewModel
    {
        public int Id { get; set; }
        public string OwnerProfile { get; set; }
        public string OwnerName { get; set; }
        public string UserComment { get; set; }
    }
}
