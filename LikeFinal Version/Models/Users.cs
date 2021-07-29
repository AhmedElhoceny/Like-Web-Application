using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LikeFinal_Version.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoto { get; set; }
        public int Coins { get; set; }
        public string BirthDay { get; set; }
        public int NumberOfLuck { get; set; }
        public int DayOfYear { get; set; }
    }
}
