using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class Likes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PostedId { get; set; }
        public string LikeUserId { get; set; }
        public DateTime LikeTimeStamp { get; set; }
    }
}
