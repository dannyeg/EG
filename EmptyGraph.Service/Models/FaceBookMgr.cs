using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace EmptyGraph.Service.Models
{
    public class FaceBookMgr
    {
        private FacebookClient _facebookClient;
        

        //public FaceBookMgr()
        //{
        //    _facebookClient = new FacebookClient("CAAXBGiZC3BpwBAOmyO9xxpCpirSCbsqICRtk6p9HyWbRlZCRhYHz0MZB5kiOU1uoB30J67VOXlhcx4KwWeACuYuCUg7ZCDsOZAi7fetdtJ33Aw0e54PZBF888307EgMy3ZBZBKYc8SLp051R3lWqiU6tfVJ41kDUc60iPsld8lCsY94cIZBYMoVaZA5CfsSS9EHfqmPaCCrKHjguuZCPQvSQuZBT");
        //}
        public FaceBookMgr(string token)
        {
            _facebookClient = new FacebookClient(token);
        }

        public dynamic GetFriends()
        {
            dynamic friends = _facebookClient.Get("me?fields=friends");

            if (friends.friends == null) return null;

            return friends.friends.data;
        }

        
        public dynamic GetLikes()
        {
            dynamic likes = _facebookClient.Get("me?fields=likes");
            if (likes.likes == null) return null;
            return likes.likes.data;
        }

        public dynamic GetMe()
        {
            return _facebookClient.Get("me");
        }


        public dynamic GetUser(string facebookId)
        {
            return _facebookClient.Get(facebookId);
        }
    }
}