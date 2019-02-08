using AuthApplication.Models;
using AuthApplication.Utils;
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
        static List<Computer> comps = new List<Computer>();
        static HomeController()
        {
            comps.Add(new Computer { Id = 1, Name = "Apple II", Company = "Apple", Year = 1977 });
            comps.Add(new Computer { Id = 2, Name = "Macintosh", Company = "Apple", Year = 1983 });
            comps.Add(new Computer { Id = 3, Name = "IBM PC", Company = "IBM", Year = 1981 });
            comps.Add(new Computer { Id = 4, Name = "Altair", Company = "MITS", Year = 1975 });
        }

        //[ClaimsAuthorize(Age=18)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Comps()
        {
            return View(comps);
        }

        public ActionResult CompDetail(int id)
        {
            Computer c = comps.FirstOrDefault(x => x.Id == id);
            if (c!=null)
            {
                return PartialView(c);
            }
            return HttpNotFound();
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

        //[ClaimsAuthorize(Age = 22)]
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

        public class Computer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Company { get; set; }
            public int Year { get; set; }
        }
    }
}