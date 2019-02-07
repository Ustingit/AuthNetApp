using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AuthApplication.Utils
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public int Age { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            ClaimsIdentity claimsIdentity;
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            var yearClaims = claimsIdentity.FindFirst("Year");
            if (yearClaims == null)
            {
                return false;
            }

            int year;
            if (!Int32.TryParse(yearClaims.Value, out year))
            {
                return false;
            }

            // проверяем возраст относительно текущей даты
            if ((DateTime.Now.Year - year) < this.Age)
            {
                return false;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}