using EmtpyGraph.Neo4J.Properties;
using Neo4jClient;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient.Cypher;
using Newtonsoft.Json.Linq;

namespace EmtpyGraph.Neo4J
{
    public class Neo4JManager
    {
        private GraphClient _graphClient;

        public Neo4JManager()
        {
            _graphClient = new GraphClient(new Uri(Settings.Default.Uri), Settings.Default.UserName, Settings.Default.Password);
            _graphClient.Connect();
        }

        public void AddOrUpdateUser(dynamic user)
        {
            string id = user.id;
            //var u = GetUser(id);
            var u = GetNodeById(id, "User");
            if (u == null)
            {
                //AddUser(user);
                AddNode(user, "User");
            }
            else
            {
                UpdateUser(user);
            }
        }

        private void UpdateUser(dynamic userToUpdate)
        {
            string userId = userToUpdate.id;

            _graphClient.Cypher
                .Match("(user:User)")
                .Where((User user) => user.id == userId)
                .Set("user = {updateUser}")
                .WithParam("updateUser", userToUpdate)
                .ExecuteWithoutResults();
        }
        private void UpdateNode(dynamic nodeToUpdate, string nodeType, string id)
        {
            var match = String.Format("(node:{0})", nodeType);

            _graphClient.Cypher
                .Match(match)
                .Where((Node node) => node.id == id)
                .Set("node = {updateNode}")
                .WithParam("updateNode", nodeToUpdate)
                .ExecuteWithoutResults();
        }
        private void AddUser(dynamic user)
        {
            _graphClient.Cypher
                .Create("(user:User {newUser})")
                .WithParam("newUser", user)
                .ExecuteWithoutResults();
        }

        public void AddNode(dynamic node, string nodeType)
        {
            var newNodeName = "new" + nodeType;
            var create = String.Format("({0}:{1}", "node", nodeType) + "{" + newNodeName + "})";

            try
            {
                _graphClient.Cypher
                    .Create(create)
                    .WithParam(newNodeName, node)
                    .ExecuteWithoutResults();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //_graphClient.Cypher
            //    .Create("(user:User {newUser})")
            //    .WithParam("newUser", node)
            //    .ExecuteWithoutResults();
        }

        public dynamic GetNodeById(string id, string nodeType)
        {
            try
            {
                var match = String.Format("(node:{0})", nodeType);

                var returnNode = _graphClient.Cypher.Match(match)
                    .Where((Node node) => node.id == id)
                    .Return(node => node.As<Node>()).Results.FirstOrDefault();
                return returnNode;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }
        public dynamic GetUser(string id)
        {
            try
            {
                var updateUser = _graphClient.Cypher.Match("(user:User)")
                    .Where((User user) => user.id == id)
                    .Return(user => user.As<User>()).Results.FirstOrDefault();
                return updateUser;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }
        public dynamic GetRelationships(string nodeType)
        {
//MATCH(n: User) -[r]->()
//RETURN type(r), count(*)

            var match = String.Format("(n:{0}) -[r]]>()", nodeType);

            ICypherFluentQuery<string> query = _graphClient.Cypher
                .Match(match)
                .Return(n => type(r))
                //.Return(n => n.As<string>());

            if (query.Results.Any())
            {
                var s = new StringBuilder();
                s.AppendLine("{");
                s.AppendFormat(" \"{0}\": ", containerLabel).AppendLine();
                s.AppendLine("      [");

                var first = true;

                foreach (var result in query.Results)
                {
                    if (!first)
                    {
                        s.AppendLine(",");
                    }
                    else
                    {
                        first = false;
                    }

                    s.AppendLine(result);
                }
                s.AppendLine("      ]");
                s.AppendLine("}");

                return s.ToString();
            }
            return null;
        }

        #region archive
        //var updateUser = _graphClient.Cypher.Match("(user:User)")
        //    .Where((User u) => u.id == id)
        //    .Return(u => u.As<User>()).Results.Single();
        //return updateUser;

        //public void Test()
        //{
        //    var newUser = new User {id = "1", name = "Dan" };
        //    _graphClient.Cypher
        //        .Create("(user:User {newUser})")
        //        .WithParam("newUser", newUser)
        //        .ExecuteWithoutResults();

        //    var updateUser = _graphClient.Cypher.Match("(user:User)")
        //        .Where((User user) => user.id == newUser.id)
        //        .Return(user => user.As<User>()).Results.Single();

        //    updateUser.email = "asdf@asdf.com";

        //    _graphClient.Cypher
        //        .Match("(user:User)")
        //        .Where((User user) => user.id == newUser.id)
        //        .Set("user = {updateUser}")
        //        .WithParam("updateUser", updateUser)
        //        .ExecuteWithoutResults();
        //}
        //public void TestQuery()
        //{
        //    var results =_graphClient.Cypher
        //        .Match("(user:User)")
        //        .Where((User user) => user.name == "Dan")
        //        .Return(user => user.As<User>())
        //        .Results;
        //    //var results = _graphClient.Cypher
        //    //    .Match("(p:Person)")
        //    //    .Return(person => person.As<Person>())
        //    //    .Results;
        //}

        //        public string AddOrUpdateUserOLD(dynamic user)
        //        {
        //            //TODO: add duplicate check
        //            var workingUser = GetFBUser(user.id);
        //            if (workingUser == null)
        //            {
        //                workingUser = new User();
        //            }

        //            workingUser.id = user.id;
        //            workingUser.name = user.name;
        //            workingUser.first_name = user.first_name;
        //            workingUser.last_name = user.last_name;
        //            workingUser.middle_name = user.middle_name;
        //            workingUser.about = user.about;
        //            workingUser.age_range = user.age_range;
        //            workingUser.address = user.address;
        //            workingUser.bio = user.bio;
        //            workingUser.birthday = user.birthday;
        //            workingUser.devices = user.devices;
        //            workingUser.email = user.email;
        //            workingUser.favorite_athletes = user.favorite_athletes;
        //            workingUser.favorite_teams = user.favorite_teams;
        //            workingUser.gender = user.gender;
        //            workingUser.political = user.political;
        //            workingUser.relationship_status = user.relationship_status;
        //            workingUser.religion = user.religion;
        //            workingUser.significant_other = user.significant_other;

        ////TODO This isn't going to work, need to structure the set differently
        ////TODO Look for other examples, see if the whole object can be passed in
        ////TODO Otherwise just build it dynamically and do the update
        //            _graphClient.Cypher
        //                .Set("(user:User {newUser})")
        //                .WithParam("newUser", workingUser)
        //                .ExecuteWithoutResults();

        //            return "";

        //            //var userExists = DoesFBUserExist(user.id);
        //            //if (userExists)
        //            //{
        //            //    var x = 1;
        //            //}
        //            //else
        //            //{
        //            //    var y = 2;
        //            //}
        //            /*


        //            //TODO: Should serialize the request each time when going live
        //            _log.AddEntry(me.id, "me", JsonConvert.SerializeObject(me));

        //            var user = new User();
        //            user.id = me.id;
        //            user.name = me.name;
        //            user.first_name = me.first_name;
        //            user.last_name = me.last_name;
        //            user.about = me.about;
        //            //user.address =
        //            user.age_range = me.age_range;
        //            user.about = me.about;
        //            user.address = me.address;
        //            user.bio = me.bio;
        //            user.birthday = me.birthday;
        //            user.devices = me.devices;
        //            user.email = me.email;
        //            user.favorite_athletes = me.favorite_athletes;
        //            user.favorite_teams = me.favorite_teams;
        //            user.first_name = me.first_name;
        //            user.gender = me.gender;
        //            user.inspirational_people = me.inspirational_people;
        //            if (me.installed != null)
        //            {
        //                user.installed = me.installed;
        //            }
        //            user.interested_in = me.interested_in;
        //            if (me.is_shared_login != null)
        //            {
        //                user.is_shared_login = me.is_shared_login;
        //            }
        //            if (me.is_verified != null)
        //            {
        //                user.is_verified = me.is_verified;
        //            }
        //            user.languages = me.languages;
        //            user.last_name = me.last_name;
        //            user.link = me.link;
        //            user.locale = me.locale;
        //            user.middle_name = me.middle_name;
        //            user.name = me.name;
        //            user.name_format = me.name_format;
        //            user.payment_pricepoints = me.payment_pricepoints;
        //            //user.test_group = me.test_group; causing error and probably worthless
        //            user.political = me.political;
        //            user.relationship_status = me.relationship_status;
        //            user.religion = me.religion;
        //            user.significant_other = me.significant_other;
        //            user.sports = me.sports;
        //            user.quotes = me.quotes;
        //            user.third_party_id = me.third_party_id;
        //            if (me.timezone != null)
        //            {
        //                user.timezone = me.timezone;
        //            }



        //            */
        //            //    var newUser = new User
        //            //    {
        //            //        id = user.id,
        //            //        name = user.name,
        //            //        first_name = user.first_name,
        //            //        last_name = user.last_name,
        //            //        middle_name = user.middle_name,
        //            //        about = user.about,
        //            //        age_range = user.age_range,
        //            //        address = user.address,
        //            //        bio = user.bio,
        //            //        birthday = user.birthday,
        //            //        devices = user.devices,
        //            //        email = user.email,
        //            //        favorite_athletes = user.favorite_athletes,
        //            //        favorite_teams = user.favorite_teams,
        //            //        gender = user.gender,
        //            //        political = user.political,
        //            //        relationship_status = user.relationship_status,
        //            //        religion = user.religion,
        //            //        significant_other = user.significant_other

        //            //};
        //        }

        //private bool DoesFBUserExist(string facebookId)
        //{

        //    //MATCH (n:User {id:'4'}) return n
        //    var match = String.Concat("(user:User {id:'", facebookId, "'})");

        //    var results = _graphClient.Cypher
        //        .Match(match)
        //        .Where((User user) => user.id == facebookId)
        //        .Return(user => user.As<User>())
        //        .Results;

        //    return results.Any();
        //}
        //private User GetFBUser(string facebookId)
        //{

        //    //MATCH (n:User {id:'4'}) return n
        //    var match = String.Concat("(user:User {id:'", facebookId, "'})");

        //    var results = _graphClient.Cypher
        //        .Match(match)
        //        .Where((User user) => user.id == facebookId)
        //        .Return(user => user.As<User>())
        //        .Results;

        //    return results.First();
        //}

        #endregion


        public void AddRelationship(dynamic user, dynamic friend)
        {
            _graphClient.Cypher
                .Match("(a:User),(b:User)")
                .Where("a.id='" + user.id + "' and b.id ='" + friend.id + "'")
                .Create("(a)-[r:isfriend]->(b)")
                .ExecuteWithoutResults();
        }

        public void AddRelationship(string node1Type, string id1, string node2Type, string id2, string relationShipName)
        {
            var match = String.Format("(a:{0}),(b:{1})", node1Type, node2Type);
            var create = String.Format("(a)-[r:{0}]->(b)", relationShipName);
            var where = "a.id='" + id1 + "' and b.id ='" + id2 + "'";

            try
            {
                _graphClient.Cypher
                    .Match(match)
                    .Where(where)
                    .Create(create)
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }
        }

        public string GetNodes(string nodetype, string containerLabel)
        {
            var match = String.Format("(n:{0})", nodetype);

            ICypherFluentQuery<string> query = _graphClient.Cypher
                .Match(match)
                .Return(n => n.As<string>());

            if (query.Results.Any())
            {
                var s = new StringBuilder();
                s.AppendLine("{");
                s.AppendFormat(" \"{0}\": ", containerLabel).AppendLine();
                s.AppendLine("      [");

                var first = true;

                foreach (var result in query.Results)
                {
                    if (!first)
                    {
                        s.AppendLine(",");
                    }
                    else
                    {
                        first = false;
                    }

                    s.AppendLine(result);
                }
                s.AppendLine("      ]");
                s.AppendLine("}");
                
                return s.ToString();
            }

            return null;
#region old code
            //dynamic test = query.Results;



            {


                //TODO: create overrides for GetNodes to add filter, etc

                //var results = _graphClient.Cypher
                //    .Match("(login:Login)")
                //    .Return<Node>("login")
                //    .Results;



                //        var query = graphClient
                //.Cypher
                //.Start("n", node)           // START n=node(123)
                //.Match("n--x")              // MATCH n--x
                //.Return<string>("x.Name");

                //return null;
                //        .Where((User user) => user.id == facebookId)
                //        .Return(user => user.As<User>())

                //    var results = _graphClient.Cypher
                //        .Match(match)
                //        .Where((User user) => user.id == facebookId)
                //        .Return(user => user.As<User>())
                //        .Results;
            }
#endregion

        }

        public async void GetNodesRest()
        {

            var client = new HttpClient();
            try
            {
                
                string responseBody = await client.GetStringAsync("http://localhost:7474/db/data/transaction/");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

    }
    


    public class User
    {
        public string id { get; set; }
    }

    public class Node
    {
        public string id { get; set; }
        public string Test { get; set; }
    }
}
