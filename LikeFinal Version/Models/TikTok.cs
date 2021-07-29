using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class TikTok
    {
        [Key]
        public int ItemID { get; set; }
        public int UserNumber { get; set; }
        public string ItemLink { get; set; }
        public string ImgUrl { get; set; }
        public string ItemTybe { get; set; }
        public int CoinsNumber { get; set; }
        public int IsClicked { get; set; }
        public int VideoTime { get; set; }
    }
}
