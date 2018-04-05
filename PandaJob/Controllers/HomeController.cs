using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PandaJob.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() 
        {
            return View();
        }

        public JsonResult SignIn(string name, string password)
        {
            if (name == "admin" && password == "1234")
            {
                //form认证
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddDays(7), false, name);
                string str = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str);
                cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);
                return Json(new { Code = 200, Message = "success" });
            }

            var r = new
            {
                Code = 100,
                Message = "failure"
            };
            return Json(r);
        }
    }
}