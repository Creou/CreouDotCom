using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreouDotCom.ViewModels;

namespace CreouDotCom.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Cv()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }

    }
}
