using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeVersionNew.Models
{
    interface SocialMediaInterface<Item>
    {
        List<Item> GetAllUsers();
        void AddYoutubeItem(Item YoutubeItem);
        Item FindYoutubeItemByID(int id);
        void RemoveYooutubeItem(int id);
        void EditYOutubeItem(Item Data);
        List<Item> GetAllUserLikes(int id);
        List<Item> GetAllUserComments(int id);
        List<Item> GetAllUserSubscribes(int id);
        List<Item> GetAllUserViews(int id);
        List<Item> GetAllLikes(int id);
        List<Item> GetAllComments(int id);
        List<Item> GetAllSubscribes(int id);
        List<Item> GetAllViews(int id);
        int GetVideoData(string VideoURL, string StatisticsType);
        int GetChannelData(string ChannelURL);
        int GetChannelDataCheck(string ChannelURL);
        int GetVideoDataCheck(string VideoURL);
    }
}
