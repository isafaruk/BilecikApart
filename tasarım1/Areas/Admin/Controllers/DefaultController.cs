using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasarım1.Areas.Admin.Models.DataAccessLayer;
using tasarım1.Attribute;
using tasarım1.Models;

namespace tasarım1.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        [FilterLog]
        public ActionResult Apart()
        {
            IEnumerable<Apartlar> apart = ApartAccess.GetAllApart();
            return View(apart);
        }
        [FilterLog]
        // GET: Admin/Default/Details/5
        public ActionResult Details(int id)
        {
            Apartlar apartDetail = ApartAccess.GetApartlar(id);
            return View(apartDetail);
        }

        [FilterLog]
        // GET: Admin/Default/Create
        public ActionResult Create()
        {
            return View();
        }

        [FilterLog]
        // POST: Admin/Default/Create
        [HttpPost]
        public ActionResult Create(Apartlar apart, List<HttpPostedFileBase> file)
        {
            try
            {
                
                ApartAccess.AddApart(apart, file);
                return RedirectToAction("Apart");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Default/Edit/5
        [FilterLog]
        public ActionResult Edit(int id)
        {
            Apartlar apart = ApartAccess.GetApartlar(id);
            return View(apart);
        }

        // POST: Admin/Default/Edit/5
        [HttpPost]
        [FilterLog]
        public ActionResult Edit(Apartlar apart)
        {
            try
            {
                // TODO: Add update logic here
                ApartAccess.EditApart(apart);
                return RedirectToAction("Apart");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Default/Delete/5
        [FilterLog]
        public ActionResult Delete(int id)
        {
            Apartlar apart = ApartAccess.GetApartlar(id);
            return View(apart);
        }

        // POST: Admin/Default/Delete/5
        [HttpPost]
        [FilterLog]
        public ActionResult Delete(Apartlar apart)
        {
            try
            {
                ApartAccess.DeleteApart(apart.id);
                return RedirectToAction(nameof(Apart));
            }
            catch
            {
                return View();
            }
        }
        
    }
}
