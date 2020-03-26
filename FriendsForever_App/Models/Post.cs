using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string PostedId { get; set; }
        public string PostedContent { get; set; }
        public string PostedOwnerUserId { get; set; }
        public string PostedOwnerPhoto { get; set; }
        public string PostedOwnerName { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string PhotoAttached { get; set; }
        public DateTime PostedTimeStamp { get; set; }
    }
}
