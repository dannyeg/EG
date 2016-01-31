using System.Web.Http;
using EmtpyGraph.Neo4J;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace EmptyGraph.Service.Controllers
{
    public class UserController : ApiController
    {
        private Neo4JManager _neo4J = new Neo4JManager();
        [EnableCors(origins: "http://localhost:49615", headers: "*", methods: "*")]
        public JToken Get()
        {
            var logins = _neo4J.GetNodes("User", "Users");
            
            JToken testJToken = JObject.Parse(logins);

            return testJToken;
        }
    }
}
