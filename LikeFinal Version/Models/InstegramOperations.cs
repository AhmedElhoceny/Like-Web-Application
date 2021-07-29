﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class InstegramOperations
    {
        [Key]
        public int OperationID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
    }
}
