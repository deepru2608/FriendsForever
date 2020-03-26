using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.Models
{
    public class QualityParameterTable
    {
        [Key]
        public int ParamId { get; set; }
        public string ParamMessage { get; set; }
    }
}
