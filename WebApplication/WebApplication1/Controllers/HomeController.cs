using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult About(int seconds=1)
        {
            var sent= IoTHubHelper.SendDirectMethod(seconds);

            if (sent) { ViewBag.Result = "Message sent! "; }
            else { ViewBag.Result = "Error sending."; }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}