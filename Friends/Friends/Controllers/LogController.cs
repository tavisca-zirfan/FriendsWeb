using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Friends.Controllers
{
    public class LogController : ApiController
    {
        //
        // GET: /Log/
        [HttpGet]
        public string Index()
        {
            return "log";
        }

    }
}
