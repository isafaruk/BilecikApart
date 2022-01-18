using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasarım1.Areas.Admin.Models;
using tasarım1.Areas.Admin.Models.DataAccessLayer;

namespace tasarım1.Areas.Admin.Controllers
{
    public class LoggerController : Controller
    {
        // GET: Admin/Logger
        public ActionResult GetLog()
        {
            IEnumerable <Logger> users = LoggerAccess.GetAllLogger();
            return View(users);
        }
    }
}
