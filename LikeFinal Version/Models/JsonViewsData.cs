using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class JsonViewsData
    {
        public String VideoURL { get; set; }
        public int VideoTime { get; set; }
        public int VideoOwnerID { get; set; }
        public int ClientID { get; set; }
        public int CoinsNumber { get; set; }
        public int ItemID { get; set; }
        public int ItemPreviousViews { get; set; }
    }
    class TheJsonResult
    {
        public string ReturnMessage { get; set; }
    }
}
