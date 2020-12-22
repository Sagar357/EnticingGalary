using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnticingGalary.Models;
using EnticingGalary.BAL;
using PagedList;
using System.Data;

namespace EnticingGalary.Controllers
{
    public class HomeController : Controller
    {
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();
        CrudCategoryBAL objCrudCategoryBAL = new CrudCategoryBAL();
        public ActionResult Index()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            //Trending Newcategory
            ViewBag.NewCategoryTypeName = db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).Take(8).ToList();
            //Feature Category List
            ViewBag.MainCategoryList = db.CategoryTypes.ToList();

            //Wallpapre Albums
            DataTable dt = objCrudCategoryBAL.GetWallpaperAlbums();
            ViewBag.WallpaperAlbums = dt.AsEnumerable();

            //Most View Wallpaper Albums
            DataTable dtmvwa = objCrudCategoryBAL.GetMostViewWallpaperAlbums();
            ViewBag.MostViewWallpaperAlbums = dtmvwa.AsEnumerable();

            //Get Home Page Content
            var getHomepageContent = db.WallpaperHomePageContents.Where(m => m.WHPContentId == 1).FirstOrDefault();
            ViewBag.TrendingThisWeekContent = getHomepageContent.TrendingThisWeekContent;
            ViewBag.FeaturedCategoryContent = getHomepageContent.FeaturedCategoryContent;
            ViewBag.TrendingWallpaperAlbumsContent = getHomepageContent.TrendingWallpaperAlbumsContent;
            ViewBag.MostViewWallpaperAlbumsContent = getHomepageContent.MostViewWallpaperAlbumsContent;


            return View();
        }
        public ActionResult About()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View();
        }
        public ActionResult Contact()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactEnquiry contactEnquiry)
        {
            if (ModelState.IsValid)
            {
                contactEnquiry.CreatedOn = DateTime.Now;
                db.ContactEnquiries.Add(contactEnquiry);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Thank You! Our support team will contact you soon.";
                ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");
                return View();
            }
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View(contactEnquiry);
        }

        public ActionResult Privacypolicy()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View();
        }
        public ActionResult Termsandcondition()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            return View();
        }

        public ActionResult Sitemap()
        {
            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            //Main Category
            ViewBag.MainCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            //Sub Category
            ViewBag.SubCategory = db.Categories.OrderBy(m => m.CategoryName).ToList();

            return View();
        }

        public ActionResult Faq()
        {
            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();

            ViewBag.FaqList = db.WallFaqs.OrderByDescending(m=>m.FaqId).ToList();

            return View();
        }

        public ActionResult Searchcategory(string id)
        {
            id = id.Replace(" ", "-");
            return RedirectToAction("SubMainCategory", "Category", new { SEOMainCategoryName = id });
        }




        [HttpPost]
        public JsonResult GetCategory(string Prefix)
        {
            //Note : you can bind same list from database  
            var getcategoryName = db.CategoryTypes.Where(m => m.CategoryTypeName.Contains(Prefix)).ToList();

            return Json(getcategoryName, JsonRequestBehavior.AllowGet);
        }
    }
}