using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace EmptyGraph.Service.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //http://localhost:55412/api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }
        [System.Web.Mvc.HttpGet]
        public JsonResult<string> GetAllLogins()
        {
            return Json("[{'a':'1','b':'2'},{'a':'4','b':'3'}]");
        }
 
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
