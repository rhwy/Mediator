using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mediator.Sample.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Mediator demo!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult MessageSample()
        {
            return View();
        }

        public ActionResult ChatSample()
        {
            return View();
        }
    }
}
