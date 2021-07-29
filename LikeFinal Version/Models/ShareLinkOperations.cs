using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class ShareLinkOperations
    {
        [Key]
        public int ID { get; set; }
        public int NewPartner { get; set; }
        public int OldPartner { get; set; }
    }
}
