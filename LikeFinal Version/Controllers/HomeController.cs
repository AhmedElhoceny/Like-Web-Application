using Like.Models;
using LikeFinal_Version.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Like.Controllers
{

    public class HomeController : Controller
    {
        public UserRepo UserMethods { get; }
        public YoutubeRepo YoutubeMethods { get; }
        public ContextData MyDB { get; }
        public IHostingEnvironment Hosting { get; }

        public HomeController(UserRepo UserMethods, IHostingEnvironment hosting , YoutubeRepo YoutubeMethods , ContextData MyDB )
        {
            this.UserMethods = UserMethods;
            Hosting = hosting;
            this.YoutubeMethods = YoutubeMethods;
            this.MyDB = MyDB;
        }
        // GET: HomeController
        public ActionResult Index(string messege)
        {
            if (messege != null)
            {
                ViewBag.messege = messege;
            }
            else
            {
                ViewBag.messege = null;
            }
            return View();
        }

        public ActionResult ArabicPage()
        {
            return View();
        }

        public ActionResult SignUp(string UserName, string Email, string Password, string date, IFormFile Image)
        {
            List<Users> OurUsers = UserMethods.GetAllUsers();
            foreach (var user in OurUsers)
            {
                if(user.UserEmail == Email)
                {
                    return RedirectToAction(nameof(Index) , new { messege = "Your Email is Already Exist :)" });
                }
            }
            string upload = Path.Combine(Hosting.WebRootPath, "Profiles");
            string fileName = Image.FileName;
            string FullPath = Path.Combine(upload, Image + fileName);
            Image.CopyTo(new FileStream(FullPath, FileMode.Create));
            UserMethods.AddUser(new Users() { BirthDay = date, Coins = 30, UserEmail = Email, UserName = UserName, UserPassword = Password, UserPhoto = fileName });
            return RedirectToAction(nameof(Index));
        }
        public ActionResult LogIn(string Email, string Password)
        {
            Users SearchedUser = UserMethods.FindUserByEmail(Email);
            if (SearchedUser != null)
            {
                if (SearchedUser.UserPassword == Password)
                {
                    return RedirectToAction("Profile", new { id = SearchedUser.UserID });
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { messege = "Wrong PassWord" });
                }
            }
            else
            {
                return RedirectToAction(nameof(Index), new { messege = "Not Exist" });
            }
        }
        public ActionResult Profile(int id)
        {
            Users SearchedUser = UserMethods.FindUserByID(id);
            return View(SearchedUser);
        }
        public ActionResult ProfilePost( int id, string CurrentPassword , string NewPassword)
        {
            Users SearchedUser = UserMethods.FindUserByID(id);
            if(SearchedUser.UserPassword == CurrentPassword)
            {
                SearchedUser.UserPassword = NewPassword;
                UserMethods.EditUserData(SearchedUser);
                return RedirectToAction(nameof(Profile) , new { id = id});
            }
            else
            {
                return RedirectToAction(nameof(Index), new { messege = "Wrong PassWord" });
            }
        }
        public ActionResult YouTubeViews(int id)
        {
            List<Youtubes> ClientViews = MyDB.Youtubes.Where(x => x.UserNumber == id && x.ItemTybe == "viewCount").ToList();
            List<ViewModel2> ClientDataViews = new List<ViewModel2>();
            foreach (var ClientItem in ClientViews)
            {
                ClientDataViews.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Views") });
            }
            ViewModel1 AllData = new ViewModel1() {  UserData = ClientDataViews };
            ViewModel3 FinalData = new ViewModel3() { AllData = AllData, Client = UserMethods.FindUserByID(id) };
            return View(FinalData);
        }
        public ActionResult YouTubeViewsPost(int ClientID , string ItemLink , int Coins , int VideoTime)
        {
            if (ItemLink.Contains("https://www.youtube.com/watch?v=") && Coins > 0 && VideoTime > 0 && YoutubeMethods.GetVideoDataCheck(ItemLink) == 1  )
            {
                YoutubeMethods.AddYoutubeItem(new Youtubes() { CoinsNumber = Coins, ImgUrl = "Null", IsClicked = 0, ItemLink = ItemLink, ItemTybe = "viewCount", UserNumber = ClientID, VideoTime = VideoTime });
            }
            return RedirectToAction(nameof(YouTubeViews), new { id = ClientID });
        }
        public JsonResult GetAllYouTubeViews(int id)
        {
            List<Youtubes> GeneralViews = MyDB.Youtubes.Where(x => x.UserNumber != id && x.ItemTybe == "viewCount").ToList();
            foreach (var Oper in MyDB.Operations.ToList())
            {
                foreach (var GeneralItem in GeneralViews.ToList())
                {
                    if (Oper.ItemID == GeneralItem.ItemID && Oper.UserID == id)
                    {
                        GeneralViews.Remove(GeneralItem);
                    }
                }
            }
            foreach (var GeneralItem in GeneralViews.ToList())
            {
                Users VideoOwner = UserMethods.FindUserByID(GeneralItem.UserNumber);
                if (VideoOwner.Coins < GeneralItem.CoinsNumber)
                {
                    GeneralViews.Remove(GeneralItem);
                }
            }
            List<ViewModel2> GeneralDataViews = new List<ViewModel2>();
            foreach (var ClientItem in GeneralViews)
            {
                GeneralDataViews.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Views") });
            }
            if(GeneralDataViews.Count >0)
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = id, VideoOwnerID = GeneralDataViews[0].YoutubeData.UserNumber, VideoURL = GeneralDataViews[0].YoutubeData.ItemLink, VideoTime = GeneralDataViews[0].YoutubeData.VideoTime , CoinsNumber = GeneralDataViews[0].YoutubeData.CoinsNumber , ItemID = GeneralDataViews[0].YoutubeData.ItemID , ItemPreviousViews = GeneralDataViews[0].Value };
                return Json(SentData);
            }
            else
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = 0, VideoOwnerID = 0, VideoURL = null, VideoTime = 0 , CoinsNumber = 0 , ItemID = 0 , ItemPreviousViews = 0 };
                return Json(SentData);
            }   
        }
        public ActionResult ViewsDone(int TheClientID , int TheVideoOwnerID , int TheCoinsNumber )
        {
            Users VideoOwner = UserMethods.FindUserByID(TheVideoOwnerID);
            Users OurClinet = UserMethods.FindUserByID(TheClientID);
            VideoOwner.Coins -= TheCoinsNumber;
            OurClinet.Coins += TheCoinsNumber;
            MyDB.SaveChanges();
            TheJsonResult ResultMessage = new TheJsonResult() { ReturnMessage = "Done" };
            return Json(ResultMessage);
        }
        public ActionResult VerifyOperation(int TheClientID , int ItemmID , string Direction)
        {
            MyDB.Operations.Add(new Operation() { ItemID = ItemmID, UserID = TheClientID });
            MyDB.SaveChanges();
            switch (Direction)
            {
                case "Views":
                    return RedirectToAction(nameof(YouTubeViews), new { id = TheClientID });
                case "Likes":
                    return RedirectToAction(nameof(YoutubeLikes), new { Clientid = TheClientID });
                case "Comments":
                    return RedirectToAction(nameof(YoutubeComments), new { Clientid = TheClientID });
                case "Subscribes":
                    return RedirectToAction(nameof(YoutubeChannelsSuscribes), new { Clientid = TheClientID });
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
        public JsonResult ConfirmationYoutubeViews(string URL , int PreviousViews)
        {
            int CurrentViews = YoutubeMethods.GetVideoData(URL, "Views");
            if (CurrentViews > PreviousViews)
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Done" };
                return Json(TheResultMessage);
            }
            else
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "failed" };
                return Json(TheResultMessage);
            }
            
        }
        public ActionResult FailedViewAction(int ItemID , int ClientID , string Redirection)
        {
            MyDB.Operations.Add(new Operation() { ItemID = ItemID, UserID = ClientID });
            MyDB.SaveChanges();
            switch (Redirection)
            {
                case "Views":
                    return RedirectToAction(nameof(YouTubeViews), new { id = ClientID });
                case "Likes":
                    return RedirectToAction(nameof(YoutubeLikes), new { Clientid = ClientID });
                case "Comments":
                    return RedirectToAction(nameof(YoutubeComments), new { Clientid = ClientID });
                case "Subscribes":
                    return RedirectToAction(nameof(YoutubeChannelsSuscribes), new { Clientid = ClientID });
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult YoutubeLikes(int Clientid)
        {
            List<Youtubes> ClientViews = MyDB.Youtubes.Where(x => x.UserNumber == Clientid && x.ItemTybe == "LikesCount").ToList();
            List<ViewModel2> ClientDataLikes = new List<ViewModel2>();
            foreach (var ClientItem in ClientViews)
            {
                ClientDataLikes.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Likes") });
            }
            ViewModel1 AllData = new ViewModel1() { UserData = ClientDataLikes };
            ViewModel3 FinalData = new ViewModel3() { AllData = AllData, Client = UserMethods.FindUserByID(Clientid) };
            return View(FinalData);
        }
        public ActionResult YoutubeLikesPost(int ClientID , string ItemLink , int Coins)
        {
            if(ItemLink.Contains("https://www.youtube.com/watch?v=") && Coins > 0 && YoutubeMethods.GetVideoDataCheck(ItemLink) == 1)
            {
                YoutubeMethods.AddYoutubeItem(new Youtubes() { CoinsNumber = Coins, ImgUrl = "Null", IsClicked = 0, ItemLink = ItemLink, ItemTybe = "LikesCount", UserNumber = ClientID, VideoTime = 0 });
            }
            return RedirectToAction(nameof(YoutubeLikes), new { Clientid = ClientID });
        }
        public JsonResult GetAllYouTubeLikes(int id)
        {
            List<Youtubes> GeneralViews = MyDB.Youtubes.Where(x => x.UserNumber != id && x.ItemTybe == "LikesCount").ToList();
            foreach (var Oper in MyDB.Operations.ToList())
            {
                foreach (var GeneralItem in GeneralViews.ToList())
                {
                    if (Oper.ItemID == GeneralItem.ItemID && Oper.UserID == id)
                    {
                        GeneralViews.Remove(GeneralItem);
                    }
                }
            }
            foreach (var GeneralItem in GeneralViews.ToList())
            {
                Users VideoOwner = UserMethods.FindUserByID(GeneralItem.UserNumber);
                if (VideoOwner.Coins < GeneralItem.CoinsNumber)
                {
                    GeneralViews.Remove(GeneralItem);
                }
            }
            List<ViewModel2> GeneralDataViews = new List<ViewModel2>();
            foreach (var ClientItem in GeneralViews)
            {
                GeneralDataViews.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Likes") });
            }
            if (GeneralDataViews.Count > 0)
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = id, VideoOwnerID = GeneralDataViews[0].YoutubeData.UserNumber, VideoURL = GeneralDataViews[0].YoutubeData.ItemLink, VideoTime = GeneralDataViews[0].YoutubeData.VideoTime, CoinsNumber = GeneralDataViews[0].YoutubeData.CoinsNumber, ItemID = GeneralDataViews[0].YoutubeData.ItemID, ItemPreviousViews = GeneralDataViews[0].Value };
                return Json(SentData);
            }
            else
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = 0, VideoOwnerID = 0, VideoURL = null, VideoTime = 0, CoinsNumber = 0, ItemID = 0, ItemPreviousViews = 0 };
                return Json(SentData);
            }
        }
        public ActionResult ConfirmYoutubeLikes (string URL , int PreviousLikes )
        {
            int CurrentLikes = YoutubeMethods.GetVideoData(URL, "Likes");
            if(CurrentLikes > PreviousLikes)
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Done" };
                return Json(TheResultMessage);
            }
            else
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Failed" };
                return Json(TheResultMessage);
            }
        }
        public ActionResult YoutubeComments(int Clientid)
        {
            List<Youtubes> ClientViews = MyDB.Youtubes.Where(x => x.UserNumber == Clientid && x.ItemTybe == "CommentsCount").ToList();
            List<ViewModel2> ClientDataLikes = new List<ViewModel2>();
            foreach (var ClientItem in ClientViews)
            {
                ClientDataLikes.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Comments") });
            }
            ViewModel1 AllData = new ViewModel1() { UserData = ClientDataLikes };
            ViewModel3 FinalData = new ViewModel3() { AllData = AllData, Client = UserMethods.FindUserByID(Clientid) };
            return View(FinalData);
        }
        public ActionResult YoutubeCommentsPost(int ClientID, string ItemLink, int Coins)
        {
            if(ItemLink.Contains("https://www.youtube.com/watch?v=") && Coins > 0 && YoutubeMethods.GetVideoDataCheck(ItemLink) == 1)
            {
                YoutubeMethods.AddYoutubeItem(new Youtubes() { CoinsNumber = Coins, ImgUrl = "Null", IsClicked = 0, ItemLink = ItemLink, ItemTybe = "CommentsCount", UserNumber = ClientID, VideoTime = 0 });
            }
            return RedirectToAction(nameof(YoutubeComments), new { Clientid = ClientID });
        }
        public JsonResult GetAllYouTubeComments(int id)
        {
            List<Youtubes> GeneralViews = MyDB.Youtubes.Where(x => x.UserNumber != id && x.ItemTybe == "CommentsCount").ToList();
            foreach (var Oper in MyDB.Operations.ToList())
            {
                foreach (var GeneralItem in GeneralViews.ToList())
                {
                    if (Oper.ItemID == GeneralItem.ItemID && Oper.UserID == id)
                    {
                        GeneralViews.Remove(GeneralItem);
                    }
                }
            }
            foreach (var GeneralItem in GeneralViews.ToList())
            {
                Users VideoOwner = UserMethods.FindUserByID(GeneralItem.UserNumber);
                if (VideoOwner.Coins < GeneralItem.CoinsNumber)
                {
                    GeneralViews.Remove(GeneralItem);
                }
            }
            List<ViewModel2> GeneralDataViews = new List<ViewModel2>();
            foreach (var ClientItem in GeneralViews)
            {
                GeneralDataViews.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetVideoData(ClientItem.ItemLink, "Comments") });
            }
            if (GeneralDataViews.Count > 0)
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = id, VideoOwnerID = GeneralDataViews[0].YoutubeData.UserNumber, VideoURL = GeneralDataViews[0].YoutubeData.ItemLink, VideoTime = GeneralDataViews[0].YoutubeData.VideoTime, CoinsNumber = GeneralDataViews[0].YoutubeData.CoinsNumber, ItemID = GeneralDataViews[0].YoutubeData.ItemID, ItemPreviousViews = GeneralDataViews[0].Value };
                return Json(SentData);
            }
            else
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = 0, VideoOwnerID = 0, VideoURL = null, VideoTime = 0, CoinsNumber = 0, ItemID = 0, ItemPreviousViews = 0 };
                return Json(SentData);
            }
        }
        public ActionResult ConfirmYoutubeComments(string URL, int PreviousLikes)
        {
            int CurrentLikes = YoutubeMethods.GetVideoData(URL, "Comments");
            if (CurrentLikes > PreviousLikes)
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Done" };
                return Json(TheResultMessage);
            }
            else
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Failed" };
                return Json(TheResultMessage);
            }
        }
        public ActionResult YoutubeChannelsSuscribes(int Clientid)
        {
            List<Youtubes> ClientViews = MyDB.Youtubes.Where(x => x.UserNumber == Clientid && x.ItemTybe == "SubscribesCount").ToList();
            List<ViewModel2> ClientDataLikes = new List<ViewModel2>();
            foreach (var ClientItem in ClientViews)
            {
                ClientDataLikes.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetChannelData(ClientItem.ItemLink)});
            }
            ViewModel1 AllData = new ViewModel1() { UserData = ClientDataLikes };
            ViewModel3 FinalData = new ViewModel3() { AllData = AllData, Client = UserMethods.FindUserByID(Clientid) };
            return View(FinalData);
        }
        public ActionResult YoutubeSubscribesPost(int ClientID, string ItemLink, int Coins)
        {
            if(ItemLink.Contains("https://www.youtube.com/channel/") && Coins > 0 && YoutubeMethods.GetChannelDataCheck(ItemLink) == 1)
            {
                YoutubeMethods.AddYoutubeItem(new Youtubes() { CoinsNumber = Coins, ImgUrl = "Null", IsClicked = 0, ItemLink = ItemLink, ItemTybe = "SubscribesCount", UserNumber = ClientID, VideoTime = 0 });
            }
            return RedirectToAction(nameof(YoutubeChannelsSuscribes), new { Clientid = ClientID });
        }
        public JsonResult GetAllYouTubeChannelSubscribes(int id)
        {
            List<Youtubes> GeneralViews = MyDB.Youtubes.Where(x => x.UserNumber != id && x.ItemTybe == "SubscribesCount").ToList();
            foreach (var Oper in MyDB.Operations.ToList())
            {
                foreach (var GeneralItem in GeneralViews.ToList())
                {
                    if (Oper.ItemID == GeneralItem.ItemID && Oper.UserID == id)
                    {
                        GeneralViews.Remove(GeneralItem);
                    }
                }
            }
            foreach (var GeneralItem in GeneralViews.ToList())
            {
                Users VideoOwner = UserMethods.FindUserByID(GeneralItem.UserNumber);
                if (VideoOwner.Coins < GeneralItem.CoinsNumber)
                {
                    GeneralViews.Remove(GeneralItem);
                }
            }
            List<ViewModel2> GeneralDataViews = new List<ViewModel2>();
            foreach (var ClientItem in GeneralViews)
            {
                GeneralDataViews.Add(new ViewModel2() { YoutubeData = ClientItem, Value = YoutubeMethods.GetChannelData(ClientItem.ItemLink) });
            }
            if (GeneralDataViews.Count > 0)
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = id, VideoOwnerID = GeneralDataViews[0].YoutubeData.UserNumber, VideoURL = GeneralDataViews[0].YoutubeData.ItemLink, VideoTime = GeneralDataViews[0].YoutubeData.VideoTime, CoinsNumber = GeneralDataViews[0].YoutubeData.CoinsNumber, ItemID = GeneralDataViews[0].YoutubeData.ItemID, ItemPreviousViews = GeneralDataViews[0].Value };
                return Json(SentData);
            }
            else
            {
                JsonViewsData SentData = new JsonViewsData() { ClientID = 0, VideoOwnerID = 0, VideoURL = null, VideoTime = 0, CoinsNumber = 0, ItemID = 0, ItemPreviousViews = 0 };
                return Json(SentData);
            }
        }
        public ActionResult ConfirmYoutubeSubscribes(string URL, int PreviousLikes)
        {
            int CurrentLikes = YoutubeMethods.GetChannelData(URL);
            if (CurrentLikes > PreviousLikes)
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Done" };
                return Json(TheResultMessage);
            }
            else
            {
                TheJsonResult TheResultMessage = new TheJsonResult() { ReturnMessage = "Failed" };
                return Json(TheResultMessage);
            }
        }
        public ActionResult LuckyBox(int id)
        {
            Users SearchedUser = UserMethods.FindUserByID(id);
            DateTime today = DateTime.Today;
            if (SearchedUser.DayOfYear != today.DayOfYear)
            {
                SearchedUser.NumberOfLuck = 1;
                SearchedUser.DayOfYear = today.DayOfYear;
            }
            else
            {
                SearchedUser.NumberOfLuck = 0;
            }
            MyDB.Users.Update(SearchedUser);
            MyDB.SaveChanges();
            return View(SearchedUser);
        }
        public ActionResult LuckyBoxPost(int id , int EarnedCoinsNumber , string ActionType)
        {
            Users SearchedUser = UserMethods.FindUserByID(id);
            if(SearchedUser.Coins > 100 || SearchedUser.NumberOfLuck > 0)
            {
                if(ActionType == "Daily Action")
                {
                    SearchedUser.NumberOfLuck -= 1;
                    SearchedUser.Coins += EarnedCoinsNumber;
                    MyDB.Users.Update(SearchedUser);
                    MyDB.SaveChanges();
                    return RedirectToAction(nameof(LuckyBox), new { id = id });
                }
                else if (ActionType == "Pay Money")
                {
                    SearchedUser.Coins -= 100;
                    SearchedUser.Coins += EarnedCoinsNumber;
                    MyDB.Users.Update(SearchedUser);
                    MyDB.SaveChanges();
                    return RedirectToAction(nameof(LuckyBox), new { id = id });
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult SoonView(int id)
        {
            OurUserID UserID = new OurUserID() { OurUsserId = id };
            return View(UserID);
        }
        public ActionResult ShareLink(int id , string Message = null)
        {
            if(Message != null)
            {
                ViewBag.Message = Message;
            }
            OurUserID UserID = new OurUserID() { OurUsserId = id };
            return View(UserID);
        }
        public ActionResult ShareLinkPost(int OldUser , string Email , string PassWord)
        {
            Users SearchedUser = UserMethods.FindUserByEmail(Email);
            if(SearchedUser != null)
            {
                if(SearchedUser.UserPassword == PassWord && SearchedUser.UserID != OldUser)
                {
                    foreach (var ShareItem in MyDB.ShareLinkOperations.ToList())
                    {
                        if(ShareItem.NewPartner == SearchedUser.UserID && ShareItem.OldPartner == OldUser)
                        {
                            return RedirectToAction(nameof(ShareLink), new { id = OldUser, Message = "Failed .. Repeated Process" });
                        }
                    }
                }
                else
                {
                    return RedirectToAction(nameof(ShareLink), new { id = OldUser, Message = "Wrong PassWord OR User the Owner Link Account " });
                }
            }
            else
            {
                return RedirectToAction(nameof(ShareLink), new { id = OldUser, Message = "Wrong Email" });
            }
            ShareLinkOperations SavedProcess = new ShareLinkOperations() { NewPartner = SearchedUser.UserID, OldPartner = OldUser };
            MyDB.ShareLinkOperations.Add(SavedProcess);
            MyDB.SaveChanges();
            Users OurOldClient = UserMethods.FindUserByID(OldUser);
            OurOldClient.Coins += 50;
            MyDB.Update(OurOldClient);
            MyDB.SaveChanges();
            return RedirectToAction("Profile", new { id = SearchedUser.UserID });
        }
        public ActionResult DeleteYoutubeItem( int UserId,int ItemID , string ActionType)
        {
            YoutubeMethods.RemoveYooutubeItem(ItemID);
            List<Operation> YoutubeOperations = MyDB.Operations.ToList();
            foreach (var item in YoutubeOperations)
            {
                if(item.ItemID == ItemID)
                {
                    MyDB.Operations.Remove(item);
                    MyDB.SaveChanges();
                }
            }
            switch (ActionType)
            {
                case "Views":
                    return RedirectToAction(nameof(YouTubeViews), new { id = UserId });
                case "Likes":
                    return RedirectToAction(nameof(YoutubeLikes), new { Clientid = UserId });
                case "Comments":
                    return RedirectToAction(nameof(YoutubeComments), new { Clientid = UserId });
                case "Subscribes":
                    return RedirectToAction(nameof(YoutubeChannelsSuscribes), new { Clientid = UserId });
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}