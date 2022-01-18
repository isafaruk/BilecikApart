using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using tasarım1.Attribute;
using tasarım1.Helpers;
using tasarım1.Models.DataAccess;

namespace tasarım1.Controllers
{
    public class UserController : Controller
    {

        public static string UserName;
        // GET: User
        [FilterLog]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [FilterLog]
        public ActionResult Index(string ad, string soyad, string kullaniciadi, string telefon, string eposta, string passw, string authpassw)
        {
            int result = 0;
            if(passw == authpassw)
            {
                result = SignInAccess.UserAdd(ad, soyad, kullaniciadi, telefon, eposta, passw);
                
                if(result == 1)
                {
                    UserName = kullaniciadi;
                    EmailHelpers.OnayKoduGonder(eposta);
                    return RedirectToAction("Verification", "User");                   
                }
                else if (result == 2)
                {
                    ViewBag.MailError = "Eposta sistemde kayıtlı";
                    return View("Index");
                }
                else if (result == 3)
                {
                    ViewBag.NameError = "Kullanıcı adı sistemde kayıtlı";
                    return View("Index");
                }
                else if (result == 4)
                {
                    ViewBag.MailNameError = "Kullanıcı adı ve Eposta sistemde kayıtlı";
                    return View("Index");
                }
                else
                {
                    ViewBag.AnotherError = "Yapımcıya ulaşın.";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Error = "Parolalar eşleşmiyor.";
                return View("Index");            
            }     
        }
        
        [FilterLog]
        public ActionResult Verification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verification(string verificationCode)
        {
            if(verificationCode == EmailHelpers.OnayKoduGoster(UserName))
            {
                LoginAccess.IsActive(UserName);
                Session.Add("Success", "Kayıt Başarılı.");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("Index", "User");
            }
        }

        [FilterLog]
        public ActionResult ForgotPassword()
        {
            
            return View();
        }
        [FilterLog]
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            string result = EmailHelpers.ForgotPass(email);
            ViewBag.Message = result;
            return View();
        }

        [FilterLog]
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        [FilterLog]
        public ActionResult UserProfile(string oldPass, string newPass, string authPass)
        {
            string UserName = Session["Login"].ToString();
            int result = SignInAccess.ControlPass(UserName, oldPass);
            if(result == 1)
            {
                if(newPass == authPass)
                {
                    SignInAccess.ChangePass(UserName, newPass);
                    ViewBag.Message = "Şifre Değiştirme Başarılı.";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Şifreler Aynı Değil. ";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Şifrenizi Yanlış Yazdınız.";
                return View();
            }
        }
    }
}
