using AuthApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthApplication.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CookieTry()
        {
            FormsAuthentication.SetAuthCookie("AZAZA", true);
            return RedirectToAction("Index");
        }

        public ActionResult Users()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return View(db.Users.ToList());
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}