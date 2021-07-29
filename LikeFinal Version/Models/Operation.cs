using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class Operation
    {
        [Key]
        public int OperationID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
    }
}
