using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApiToken.Controllers
{
    public class DemoController : ApiController
    {
        [Authorize(Roles = "Massificado")]        
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "value 1", "value 2" });
        }
    }
}
