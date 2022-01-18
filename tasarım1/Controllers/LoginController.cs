using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasarım1.Attribute;
using tasarım1.Models.DataAccess;
using static tasarım1.Models.User;

namespace tasarım1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [FilterLog]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [FilterLog]
        public ActionResult Index(string kadi, string ksifre)
        {
            Session.Remove("Success");
            int result = LoginAccess.LoginControl(kadi, ksifre);
            if(result == (int)KullaniciDurum.True)
            {
                Session.Add("Login", kadi);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session.Abandon();
                ViewBag.Message = "Yanlış kullanıcı adı veya şifre";
                return View("Index");
            }
        }
        [FilterLog]
        public void Logout()
        {
            Session.Abandon();
            Response.Redirect("Index");
        }
    }
}