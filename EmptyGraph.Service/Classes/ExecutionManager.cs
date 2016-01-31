using System;
using System.Collections.Generic;
using System.Xml.Linq;
using EmtpyGraph.Neo4J;
using Facebook;
using EmptyGraph.Log;
using EmptyGraph.Service.Models;
using Microsoft.Ajax.Utilities;

namespace EmptyGraph.Engine
{
    public class ExecutionManager
    {
        private const string CATEGORY_LIST = "category_list";

        private FaceBookMgr _faceBookMgr;
        private Neo4JManager _neo4JManager;
        private EgLogManager _log;

        public ExecutionManager(EgLogManager log, string facebookToken)
        {
            _faceBookMgr = new FaceBookMgr(facebookToken);
            _neo4JManager = new Neo4JManager();
            _log = log;
        }
        
        /// <summary>
        /// Process login
        /// If user doesn't exist add
        /// If user exists, update any data we received
        /// Get Friends and add
        /// Get Likes and add
        /// </summary>
        /// <param name="userId1"></param>
        /// <param name="userId2"></param>
        public string AddTwoUsersAndFriendThem(string userId1, string userId2)
        {

            dynamic user1 = _faceBookMgr.GetUser("10152826316773636");
            _neo4JManager.AddOrUpdateUser(user1);

            dynamic user2 = _faceBookMgr.GetUser("4");
            _neo4JManager.AddOrUpdateUser(user2);

            AddRelationship("4", "10152826316773636");
            return "friends";

            //_neo4JManager.AddOrUpdateUser(me);
            ////var returnJson = Newtonsoft.Json.JsonConvert.DeserializeXNode(XNode.ReadFrom()
            //Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse("{'message':'it worked'}");
            //return jObject.ToString();

            ////TODO: Should serialize the request each time when going live
            //_log.AddEntry(me.id, "me", JsonConvert.SerializeObject(me));
        }

        public string UpdateUser(string id)
        {
            return "";
        }

        public void AddRelationship(string userId, string friendId)
        {
            dynamic user = _faceBookMgr.GetUser(userId);
            dynamic friend = _faceBookMgr.GetUser(friendId);

            _neo4JManager.AddRelationship(user, friend);

        }

        public void ProcessLoginAndAssociatedItems()
        {
            // add login node
            _neo4JManager.AddNode(new Login() {id = "15"}, "Login");

            _neo4JManager.AddRelationship("Login", "15", "User","4","created");

        }

        public void ProcessLogin()
        {
            string loginId = Guid.NewGuid().ToString();

            // get facebook user
            dynamic user = _faceBookMgr.GetMe();
            var facebookId = user.id;
            
            // add login node
            //TODO: figure out how to get an id (could let neo create, but we need to get it back to make the relationsip)
            _neo4JManager.AddNode(new Login() { id = loginId }, "Login");

            // add user if doesn't exist
            _neo4JManager.AddNode(user, "User");

            // add relationship to show that hte user was created as part of this login
            _neo4JManager.AddRelationship("Login", loginId, "User", facebookId, "created");
            
            dynamic friends = _faceBookMgr.GetFriends();
            if (friends != null)
            {
                foreach (dynamic friend in friends)
                {
                    _neo4JManager.AddNode(friend, "User");
                    _neo4JManager.AddRelationship("User", user.id, "User", friend.id, "friendsWith");
                    _neo4JManager.AddRelationship("Login", loginId, "User", friend.id, "created");
                }
                
            }

            dynamic likes = _faceBookMgr.GetLikes();
            if (likes != null)
            {
                foreach (dynamic like in likes)
                {
                    
                    ProcessLike((Facebook.JsonObject)like);
                    //var processedLike = ProcessLike((Facebook.JsonObject)like);
                    //_neo4JManager.AddNode(like, "Like");
                    _neo4JManager.AddRelationship("User", user.id, "Like", like.id, "likes");
                    _neo4JManager.AddRelationship("Login", loginId, "Like", like.id, "created");
                }
            }
        }

        private void ProcessLike(Facebook.JsonObject like)
        {
            var tempLike = ((dynamic)like);
            
            //if (tempLike.Keys.Contains(CATEGORY_LIST))
            if (HasCategoryList(tempLike))
            {
                var categories = tempLike[CATEGORY_LIST];

                IDictionary<string, object> map = tempLike;
                map.Remove(CATEGORY_LIST);

                _neo4JManager.AddNode(tempLike, "Like");

                foreach (var cat in categories)
                {
                    _neo4JManager.AddNode(cat, "Category");
                    _neo4JManager.AddRelationship("Like", tempLike.id, "Category", cat.id, "categories");

                }
            }
            else
            {
                _neo4JManager.AddNode(tempLike, "Like");
            }

        }

        private bool HasCategoryList(dynamic like)
        {
            foreach (var key in like.Keys)
            {
                if (key == CATEGORY_LIST)
                {
                    return true;
                }
            }
            return false;
        }

        #region oldcode
        //var user = new User();
        //user.id = "999";
        //user.name = "Dan Reynolds";


        //_neo4JManager.AddOrUpdateUser(user);

        //user.email = "danny@reynz.com";

        //_neo4JManager.AddOrUpdateUser(user);

        //user.birthday = "1/1/1972";

        //_neo4JManager.AddOrUpdateUser(user);


        //return "sweet";
        //var user = new User();
        //user.id = me.id;
        //user.name = me.name;
        //user.first_name = me.first_name;
        //user.last_name = me.last_name;
        //user.about = me.about;
        ////user.address =
        //user.age_range = me.age_range;
        //user.about = me.about;
        //user.address = me.address;
        //user.bio = me.bio;
        //user.birthday = me.birthday;
        //user.devices = me.devices;
        //user.email = me.email;
        //user.favorite_athletes = me.favorite_athletes;
        //user.favorite_teams = me.favorite_teams;
        //user.first_name = me.first_name;
        //user.gender = me.gender;
        //user.inspirational_people = me.inspirational_people;
        //if (me.installed != null)
        //{
        //    user.installed = me.installed;
        //}
        //user.interested_in = me.interested_in;
        //if (me.is_shared_login != null)
        //{
        //    user.is_shared_login = me.is_shared_login;
        //}
        //if (me.is_verified != null)
        //{
        //    user.is_verified = me.is_verified;
        //}
        //user.languages = me.languages;
        //user.last_name = me.last_name;
        //user.link = me.link;
        //user.locale = me.locale;
        //user.middle_name = me.middle_name;
        //user.name = me.name;
        //user.name_format = me.name_format;
        //user.payment_pricepoints = me.payment_pricepoints;
        ////user.test_group = me.test_group; causing error and probably worthless
        //user.political = me.political;
        //user.relationship_status = me.relationship_status;
        //user.religion = me.religion;
        //user.significant_other = me.significant_other;
        //user.sports = me.sports;
        //user.quotes = me.quotes;
        //user.third_party_id = me.third_party_id;
        //if (me.timezone != null)
        //{
        //    user.timezone = me.timezone;
        //}
        //user.token_for_business = me.token_for_business;
        //if (me.updated_time != null)
        //{
        //    user.updated_time = me.updated_time;
        //}
        //user.shared_login_upgrade_required_by = me.shared_login_upgrade_required_by;
        //user.verified = me.verified;
        //user.video_upload_limits = me.video_upload_limits;
        //user.viewer_can_send_gift = me.viewer_can_send_gift;
        //user.website = me.website;
        //user.work = me.work;
        //user.public_key = me.public_key;
        //user.cover = me.cover;


        //return _neo4JManager.AddOrUpdateUser(me);
        #endregion

        public string TestJson()
        {
            return _neo4JManager.GetNodes("User", "Users");
        }
    }

    public class Login
    {
        public Login()
        {
            TimeStamp = new DateTime();
        }
        public DateTime TimeStamp { get; set; }
        public string id { get; set; }

    }
}

