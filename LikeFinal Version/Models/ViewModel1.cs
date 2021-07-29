using LikeFinal_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Like.Models
{
    public class ViewModel1
    {
        public List<ViewModel2> UserData { get; set; }
    }
    public class ViewModel2
    {
        public Youtubes YoutubeData { get; set; }
        public int Value { get; set; }
    }
    public class ViewModel3
    {
        public ViewModel1 AllData { get; set; }
        public Users Client { get; set; }
    }
}
