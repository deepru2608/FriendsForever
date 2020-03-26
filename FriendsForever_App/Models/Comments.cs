using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PostedId { get; set; }
        public string Comment { get; set; }
        public string CommentedUserId { get; set; }
        public DateTime CommentedTimeStamp { get; set; }
    }
}
