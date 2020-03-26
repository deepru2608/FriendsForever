using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNo { get; set; }
        [Key]
        public string FeedBackId { get; set; }
        public string UserId { get; set; }
        public int QualityStatus { get; set; }
        public string FeedbackMessage { get; set; }
    }
}
