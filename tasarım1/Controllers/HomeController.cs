using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasarım1.Attribute;
using tasarım1.Helpers;
using tasarım1.Models;
using tasarım1.Models.DataAccess;

namespace tasarım1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [FilterLog]
        public ActionResult Index()
        {
            IEnumerable<Apartlar> apart = ApartAccess.GetAllApartWithPhoto();
            return View(apart);
        }
        [FilterLog]
        public ActionResult Apart()
        {
            IEnumerable<Apartlar> apart = ApartAccess.GetAllApart();
            return View(apart);
        }
        
        public ActionResult Details(int id)
        {
            Apartlar apartDetail = ApartAccess.GetApartlar(id);
            return View(apartDetail);
        }
        [FilterLog]
        public ActionResult Contact(string name, string email, string subject, string message )
        {
            EmailHelpers.ContactEmail(name, email,subject,message);
            return View();
        }

        [FilterLog]
        public ActionResult Search(string yazi)
        {
            IEnumerable<Apartlar> apart = ApartAccess.GetSearch(yazi);
            return View(apart);
        }

        
        [FilterLog]
        public ActionResult GetApart(string yazi)
        {
            IEnumerable<Apartlar> apart = ApartAccess.GetSearch(yazi);
            return RedirectToAction("Search", "Home", apart);
        }
    }
}