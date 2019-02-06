using AuthApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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

        public string GetInfo()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            var gender = identity.Claims.Where(c => c.Type == ClaimTypes.Gender).Select(c => c.Value).SingleOrDefault();
            var age = identity.Claims.Where(c => c.Type == "age").Select(c=> c.Value).SingleOrDefault();
            return $"<p>Эл. адрес: {email}</p><p>Пол: {gender} </p><p> Возраст: {age} </p>";
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