using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using EmptyGraph.Engine;
using EmptyGraph.Log;
using Newtonsoft.Json;
using Swashbuckle.Swagger;

namespace EmptyGraph.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExecutionController : ApiController
    {
        // GET: api/Execution
        public string Get()
        {
            return "boo";
        }

        // GET: api/Execution/5
        public JsonResult<string> Get(string id)
        {
            var log = new EgLogManager();
            var execMgr = new ExecutionManager(log, id);
            execMgr.ProcessLogin();


            Request.CreateResponse(HttpStatusCode.OK);
            var settings = new JsonSerializerSettings();

            return Json("Message sent!", settings, Encoding.Default);

            //return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST: api/Execution
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Execution/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Execution/5
        public void Delete(int id)
        {
        }
    }
}
