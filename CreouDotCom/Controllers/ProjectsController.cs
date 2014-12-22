using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreouDotCom.Controllers
{
    public partial class ProjectsController : Controller
    {
        //
        // GET: /Projects/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
