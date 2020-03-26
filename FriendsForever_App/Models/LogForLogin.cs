using FriendsForever_App.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class LogForLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }
        public string UserName { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Ip_Address { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StatusFlag { get; set; }

        [NotMapped]
        public int SerialNo { get; set; }
    }
}
