using EnticingGalary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnticingGalary.Controllers
{
    public class NotFoundController : Controller
    {
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();

        // GET: NotFound
        [HandleError]
        public ActionResult Index()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View();
        }
    }
}