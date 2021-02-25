using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class Friends
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string UserIdRequest { get; set; }
        public string UserIdResponse { get; set; }
        public string Status { get; set; }
        public DateTime Time_Stamp { get; set; }
    }
}
