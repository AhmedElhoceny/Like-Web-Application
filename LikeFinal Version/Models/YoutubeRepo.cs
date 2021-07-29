using LikeFinal_Version.Models;
using LikeVersionNew.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Like.Models
{
    public class YoutubeRepo: SocialMediaInterface<Youtubes>
    {
        private readonly ContextData myDB;

        public YoutubeRepo(ContextData MyDB)
        {
            myDB = MyDB;
        }
        public List<Youtubes> GetAllUsers()
        {
            return myDB.Youtubes.ToList();
        }
        public void AddYoutubeItem(Youtubes YoutubeItem)
        {
            myDB.Youtubes.Add(YoutubeItem);
            myDB.SaveChanges();
        }
        public Youtubes FindYoutubeItemByID(int id)
        {
            return myDB.Youtubes.Find(id);
        }
        public void RemoveYooutubeItem(int id)
        {
            myDB.Youtubes.Remove(FindYoutubeItemByID(id));
            myDB.SaveChanges();
        }
        public void EditYOutubeItem(Youtubes Data)
        {
            Youtubes searchedYoutubeItem = FindYoutubeItemByID(Data.ItemID);
            searchedYoutubeItem.CoinsNumber = Data.CoinsNumber;
            searchedYoutubeItem.ImgUrl = Data.ImgUrl;
            searchedYoutubeItem.ItemLink = Data.ItemLink;
            myDB.SaveChanges();
        }

        public List<Youtubes> GetAllUserLikes(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Likes" && x.UserNumber == id).ToList();
        }

        public List<Youtubes> GetAllUserComments(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Comments" && x.UserNumber == id).ToList();
        }

        public List<Youtubes> GetAllUserSubscribes(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Subscribes" && x.UserNumber == id).ToList();
        }

        public List<Youtubes> GetAllUserViews(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Views" && x.UserNumber == id).ToList();
        }

        public List<Youtubes> GetAllLikes(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Like" && x.CoinsNumber != id).ToList();
        }

        public List<Youtubes> GetAllComments(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Comment" && x.CoinsNumber != id).ToList();
        }

        public List<Youtubes> GetAllSubscribes(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Subscribe" && x.CoinsNumber != id).ToList();
        }
            
        public List<Youtubes> GetAllViews(int id)
        {
            return myDB.Youtubes.Where(x => x.ItemTybe == "Views" && x.CoinsNumber != id).ToList();
        }

        public int GetVideoData(string VideoURL , string StatisticsType)
        {
            var pattern = @"(?<Pre>[^-]+)v=(?<Post>.+)";
            var mtch = Regex.Match(VideoURL, pattern);
            string URL = String.Format("https://www.googleapis.com/youtube/v3/videos?key=AIzaSyCyrPI4_eSP4kC5CEiwDlIO2BDhEfwSmVc&part=statistics&id={0}", mtch.Groups["Post"]);
            WebRequest RequestObject = WebRequest.Create(URL);

            RequestObject.Method = "GET";
            HttpWebResponse ResponseObject = null;
            ResponseObject = (HttpWebResponse)RequestObject.GetResponse();
            string strResultResult = null;
            using (Stream stream = ResponseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResultResult = sr.ReadToEnd();
                var details = JObject.Parse(strResultResult);
                sr.Close();
                switch (StatisticsType)
                {
                    case "Views":
                        return (int)details["items"][0]["statistics"]["viewCount"];
                    case "Likes":
                        return (int)details["items"][0]["statistics"]["likeCount"];
                    case "Comments":
                        return (int)details["items"][0]["statistics"]["commentCount"];
                    default:
                        return 0;
                }
            }
        }

        public int GetChannelData(string ChannelURL)
        {
            var pattern = @"(?<Pre>[^-]+)/channel/(?<Post>.+)";
            var mtch = Regex.Match(ChannelURL, pattern);
            string URL = String.Format("https://www.googleapis.com/youtube/v3/channels?part=statistics&key=AIzaSyCyrPI4_eSP4kC5CEiwDlIO2BDhEfwSmVc&id={0}", mtch.Groups["Post"]);
            WebRequest RequestObject = WebRequest.Create(URL);
            RequestObject.Method = "GET";
            HttpWebResponse ResponseObject = null;
            ResponseObject = (HttpWebResponse)RequestObject.GetResponse();

            string strResultResult = null;
            using (Stream stream = ResponseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResultResult = sr.ReadToEnd();
                var details = JObject.Parse(strResultResult);
                sr.Close();
                return (int)details["items"][0]["statistics"]["subscriberCount"];
            }
        }

        public int GetChannelDataCheck(string ChannelURL)
        {

  
            var pattern = @"(?<Pre>[^-]+)/channel/(?<Post>.+)";
            var mtch = Regex.Match(ChannelURL, pattern);
            string URL = String.Format("https://www.googleapis.com/youtube/v3/channels?part=statistics&key=AIzaSyCyrPI4_eSP4kC5CEiwDlIO2BDhEfwSmVc&id={0}", mtch.Groups["Post"]);
            WebRequest RequestObject = WebRequest.Create(URL);
            RequestObject.Method = "GET";
            HttpWebResponse ResponseObject = null;
            ResponseObject = (HttpWebResponse)RequestObject.GetResponse();
            string strResultResult = null;
            using (Stream stream = ResponseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResultResult = sr.ReadToEnd();
                var details = JObject.Parse(strResultResult);
                sr.Close();
                if((int)details["pageInfo"]["totalResults"] == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


        }

        public int GetVideoDataCheck(string VideoURL)
        {
            var pattern = @"(?<Pre>[^-]+)v=(?<Post>.+)";

            var mtch = Regex.Match(VideoURL, pattern);


            string URL = String.Format("https://www.googleapis.com/youtube/v3/videos?key=AIzaSyCyrPI4_eSP4kC5CEiwDlIO2BDhEfwSmVc&part=statistics&id={0}", mtch.Groups["Post"]);
            WebRequest RequestObject = WebRequest.Create(URL);

            RequestObject.Method = "GET";
            HttpWebResponse ResponseObject = null;
            ResponseObject = (HttpWebResponse)RequestObject.GetResponse();

            string strResultResult = null;
            using (Stream stream = ResponseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResultResult = sr.ReadToEnd();

                var details = JObject.Parse(strResultResult);
                sr.Close();
                if ((int)details["pageInfo"]["totalResults"] == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }   
    }
}
