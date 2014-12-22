using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CreouDotCom.Controllers
{
    public class EmailController : ApiController
    {
        public string Get()
        {
            return @"<a href=""mailto:software@creou.com"">software@creou.com</a>";
        }
    }
}
