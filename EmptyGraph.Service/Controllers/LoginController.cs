using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EmtpyGraph.Neo4J;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace EmptyGraph.Service.Controllers
{
    public class LoginController : ApiController
    {
        private Neo4JManager _neo4J = new Neo4JManager();
        [EnableCors(origins: "http://localhost:49615", headers: "*", methods: "*")]
        public JToken Get()
        {
            var logins = _neo4J.GetNodes("Login", "Logins");
            
            JToken testJToken = JObject.Parse(logins);

            return testJToken;
        }
    }
}
