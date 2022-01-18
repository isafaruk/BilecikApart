using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasarım1.Areas.Admin.Models.DataAccessLayer;
using tasarım1.Attribute;
using static tasarım1.Models.User;

namespace tasarım1.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        [FilterLog]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [FilterLog]
        public ActionResult Index(string kname,string kpass)
        {
            int result = 0;
            result = AdminLogin.Login(kname,kpass);
            if(result == (int)KullaniciDurum.True)
            {
                Session.Add("AdminLogin", kname);
                return RedirectToAction("Apart", "Default");
            }
            else
            {
                Session.Abandon();
                ViewBag.Error = "Yanlış kullanıcı adı veya şifre";
                return View("Index");
            }
        }
        [FilterLog]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}