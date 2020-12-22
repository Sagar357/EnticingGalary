using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnticingGalary.Models;
using PagedList;
using EnticingGalary.BAL;
using System.Data;
using System.Web.UI;

/*
 * Change Log:
 * #CC01 ,Sagar Srivastava , A section on the admin page for redirection of links is to be added. So, that from the admin panel any source URL in our site can be redirected to any target URL on our website. ,29-10-2020
 */

namespace EnticingGalary.Controllers
{
    public class CategoryController : Controller
    {
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();
        CrudCategoryBAL objCrudCategoryBAL = new CrudCategoryBAL();
        // GET: Category

        public ActionResult Index()
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();


            var categorytype = from cat in db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).ToList() select cat;
            return View(categorytype);
        }

        public ActionResult SubCategory(string SEOMainCategoryName)
        {

            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();


            if (SEOMainCategoryName == "" || SEOMainCategoryName == null)
            {
                //For Search
                ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

                //Dropdown List
                ViewBag.DdlCategory = db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).ToList();
                return RedirectToAction("Index", "NotFound");
            }
            var categorytypename = db.CategoryTypes.Where(m => m.SEOCategoryTypeName == SEOMainCategoryName).FirstOrDefault();
            if (categorytypename == null)
            {
                ViewBag.SM = null;
            }
            else
            {
                ViewBag.SM = "find";
                ViewBag.MetaCTTitle = categorytypename.MetaCTTitle;
                ViewBag.MetaCTKeywords = categorytypename.MetaCTKeywords;
                ViewBag.MetaCTDescriptions = categorytypename.MetaCTDescriptions;
                ViewBag.SearchcategoryTypeName = categorytypename.CategoryTypeName;
                ViewBag.CategoryTypeContent = categorytypename.CategoryTypeContent;

                DataTable dt = objCrudCategoryBAL.GetSubCategoyGroupById(categorytypename.CategoryTypeId);
                ViewBag.SubcategoryList = dt.AsEnumerable();
            }
            return View();
        }

        public ActionResult SubCategoryDetail(string SEOCategoryName, int PageNumber )
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");
            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();


            if (SEOCategoryName == "" || SEOCategoryName == null)
            {
                //For Search
                ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

                //Dropdown List
                ViewBag.DdlCategory = db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).ToList();

                return RedirectToAction("Index", "NotFound");
            }
            var subcategory = db.Categories.Where(m => m.SEOCategoryName == SEOCategoryName).FirstOrDefault();
           

            if (SEOCategoryName != "" && SEOCategoryName != null)
            {
                /*#CC01 start*/
                if (!string.IsNullOrEmpty(subcategory.alias))
                {
                    ViewBag.alias = subcategory.alias.ToString().Replace(" ", "-") ;
                    SEOCategoryName = subcategory.alias.ToString().Replace(" ", "-");
                    return RedirectToAction("SubCategoryDetail" ,"Category" ,new { @SEOCategoryName =SEOCategoryName});
                    //subcategory = db.Categories.Where(m => m.SEOCategoryName == SEOCategoryName).FirstOrDefault();

                }
                /*#CC01 end*/

                int kk = objCrudCategoryBAL.UpdateReviewBySEOCategoryName(SEOCategoryName);
                if (kk > 0)
                {
                    ViewBag.SRM = "updated";
                }
            }
            //var subcategory = db.Categories.Where(m => m.SEOCategoryName == SEOCategoryName).FirstOrDefault();
          
            ViewBag.MetaCatTitle = subcategory.MetaCatTitle;
            ViewBag.MetaCatKeywords = subcategory.MetaCatKeywords;
            ViewBag.MetaCatDescription = subcategory.MetaCatDescription;
            ViewBag.MainCategorName = subcategory.CategoryTypeName;
            ViewBag.SubCategoryName = subcategory.CategoryName;
            ViewBag.CategoryDescription = subcategory.CategoryDescription;
            ViewBag.CategoryDescriptionBottom = subcategory.CategoryDescriptionBottom;
            var nowallpapre = db.Wallpapers.Where(m => m.SEOCatName == SEOCategoryName).FirstOrDefault();
            if (nowallpapre == null)
            {
                ViewBag.NoWallpaper = "NA";
            }
            ViewBag.SeoCatName = SEOCategoryName;
            ViewBag.WallpaperCount = db.Wallpapers.Where(m => m.SEOCatName == SEOCategoryName).ToList().Count();
            ViewBag.Pagecount = ViewBag.WallpaperCount / 10;
            ViewBag.PageNo = PageNumber;
            var wallpaperList = db.Wallpapers.Where(m => m.SEOCatName == SEOCategoryName);
            var SelectedWallpapers = wallpaperList.OrderByDescending(m=>m.CreatedOn).Skip(10*PageNumber).Take(10);

            return View(SelectedWallpapers);
        }


        //For Global Serach Like
        public ActionResult SubMainCategory(string SEOMainCategoryName)
        {
            //For Search
            ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

            //Dropdown List
            ViewBag.DdlCategory = db.CategoryTypes.OrderBy(m => m.CategoryTypeName).ToList();
            
            if (SEOMainCategoryName == "" || SEOMainCategoryName == null)
            {
                //For Search
                ViewBag.CategoryTypeName = new SelectList(db.CategoryTypes.ToList(), "SEOCategoryTypeName", "CategoryTypeName");

                //Dropdown List
                ViewBag.DdlCategory = db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).ToList();
                return RedirectToAction("Index", "NotFound");
            }

            ViewBag.SM = "find";
            ViewBag.MetaCTTitle = SEOMainCategoryName;
            ViewBag.MetaCTKeywords = SEOMainCategoryName;
            ViewBag.MetaCTDescriptions = SEOMainCategoryName;
            ViewBag.SearchcategoryTypeName = SEOMainCategoryName;
            DataTable dt = objCrudCategoryBAL.GetSubCategoyGroupLikeById(SEOMainCategoryName);
            ViewBag.SubcategoryList = dt.AsEnumerable();

            return View();
        }
    }
}