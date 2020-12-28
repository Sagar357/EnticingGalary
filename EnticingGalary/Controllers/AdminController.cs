using EnticingGalary.DAL;
using EnticingGalary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnticingGalary.ViewModels;
using EnticingGalary.BAL;
using System.Text;

namespace EnticingGalary.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();
        Encrypt_Decrypt objEncryptDecrypt = new Encrypt_Decrypt();
        AdminDAL objAdminDAL = new AdminDAL();
        CrudCategoryBAL objCrudCategoryBAL = new CrudCategoryBAL();


        // GET: /Admin/
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard", "Admin");
        }        
        public ActionResult Dashboard()
        {
            if (Session["AdminLoginId"] == null && Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            return RedirectToAction("CategoryType", "Admin");
        }
        #region add content on home page

        public ActionResult WallpaperHomePageContent()
        {
            var getHomePageContent = db.WallpaperHomePageContents.ToList();
            return View(getHomePageContent);
        }
        public ActionResult WallpaperHomePageContentDetail(long? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallpaperHomePageContent obj = db.WallpaperHomePageContents.Find(id);
            return View(obj);
        }

        public ActionResult EditWallpaperHomePageContent(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallpaperHomePageContent obj = db.WallpaperHomePageContents.Find(id);
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditWallpaperHomePageContent(WallpaperHomePageContent obj)
        {
            obj.UpdatedOn = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.SM = "edit";
            return View();

        }
        #endregion

        #region Category Type
        public ActionResult CategoryType()
        {
            return View(db.CategoryTypes.OrderByDescending(m => m.CategoryTypeId).ToList());
        }
        public ActionResult AddNewCategoryType()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewCategoryType(CategoryType categorytype, HttpPostedFileBase categorytypeimage)
        {
            if (categorytype.CategoryTypeName == "" || categorytype.CategoryTypeName == null ||
                categorytype.MetaCTTitle == "" || categorytype.MetaCTTitle == null ||
                categorytype.MetaCTDescriptions == null || categorytype.MetaCTDescriptions == "")
            {
                return View(categorytype);
            }
            var categorytypeexist = db.CategoryTypes.Where(m => m.SEOCategoryTypeName == categorytype.SEOCategoryTypeName.Replace(" ", "-")).FirstOrDefault();
            if (categorytypeexist != null)
            {
                ModelState.AddModelError("", "main category url already exist. Please enter unique url");
                return View(categorytype);
            }
            if (categorytypeimage != null && categorytypeimage.ContentLength > 0)
            {
                string getimageExtention = Path.GetExtension(categorytypeimage.FileName);
                Random rnumber = new Random();
                string FileCategoryImage = Path.GetFileName("cti" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                categorytypeimage.SaveAs(Server.MapPath("~/Content/Images/CategoryTypeImages/") + FileCategoryImage);
                string imagepath = @"https://24wallpapers.com/Content/Images/CategoryTypeImages/" + FileCategoryImage;
                //string imagepath = @"~/Content/Images/CategoryTypeImages/" + FileCategoryImage;
                categorytype.CategoryTypeBannerImage = imagepath;
                categorytype.CreatedOn = DateTime.Now;
                categorytype.SEOCategoryTypeName = categorytype.SEOCategoryTypeName.Replace(" ", "-");
                categorytype.UpdatedOn = null;
                db.CategoryTypes.Add(categorytype);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.SM = "Main caategory add successfully";
                return View();
            }
            else
            {
                return View(categorytype);
            }
        }
        public ActionResult EditCategoryType(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryType categorytype = db.CategoryTypes.Find(id);
            if (categorytype == null)
            {
                return HttpNotFound();
            }

            return View(categorytype);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCategoryType(CategoryType categorytype, HttpPostedFileBase categorytypeimage)
        {
            if (categorytype.CategoryTypeName == "" || categorytype.CategoryTypeName == null ||
                categorytype.MetaCTTitle == "" || categorytype.MetaCTTitle == null ||
                categorytype.MetaCTDescriptions == null || categorytype.MetaCTDescriptions == "")
            {
                return View(categorytype);
            }
            else if (categorytypeimage != null && categorytypeimage.ContentLength > 0)
            {
                string getimageExtention = Path.GetExtension(categorytypeimage.FileName);
                Random rnumber = new Random();
                string FileCategoryImage = Path.GetFileName("cti" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                categorytypeimage.SaveAs(Server.MapPath("~/Content/Images/CategoryTypeImages/") + FileCategoryImage);
                string imagepath = @"https://24wallpapers.com/Content/Images/CategoryTypeImages/" + FileCategoryImage;
                //string imagepath = @"~/Content/Images/CategoryTypeImages/" + FileCategoryImage;
                categorytype.CategoryTypeBannerImage = imagepath;
            }
            categorytype.UpdatedOn = DateTime.Now;
            categorytype.SEOCategoryTypeName = categorytype.SEOCategoryTypeName.Replace(" ", "-");
            db.Entry(categorytype).State = EntityState.Modified;
            db.SaveChanges();
            int i = objCrudCategoryBAL.UpdateCategoryTypeById(categorytype.CategoryTypeId, categorytype.CategoryTypeName, categorytype.SEOCategoryTypeName);
            if (i > 0)
            {
                ViewBag.SM = "updated";
            }

            CategoryType categorytypes = db.CategoryTypes.Find(categorytype.CategoryTypeId);
            if (categorytypes == null)
            {
                return HttpNotFound();
            }

            ViewBag.SM = "Main caategory edit successfully";
            return View(categorytypes);

        }
        [HttpPost]
        public JsonResult DeleteCategoryType(long? id)
        {
            int i = 0;
            CategoryType categorytype = db.CategoryTypes.Where(m => m.CategoryTypeId == id).FirstOrDefault();
            Category category = db.Categories.Where(m => m.CategoryTypeId == id).FirstOrDefault();
            Wallpaper wallpaper = db.Wallpapers.Where(m => m.MainCategoryId == id).FirstOrDefault();
            if (categorytype != null)
            {
                db.CategoryTypes.Remove(categorytype);
                i = db.SaveChanges();
            }
            int j = objCrudCategoryBAL.DeleteCategoryTypeById(id);
            if (j > 0)
            {
                ViewBag.SM = "Deleted";
            }

            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Category
        public ActionResult Category()
        {
            return View(db.Categories.OrderByDescending(m => m.CategoryId).ToList());
        }
        public ActionResult AddNewCategory()
        {
            ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult AddNewCategory(Category category, HttpPostedFileBase categoryimage)
        {
            try
            {
                if (category.CategoryName == "" || category.CategoryName == null ||
                      category.MetaCatTitle == "" || category.MetaCatTitle == null ||
                      category.SEOCategoryName == "" || category.SEOCategoryName == null ||
                      category.MetaCatDescription == "" || category.MetaCatDescription == null)
                {
                    ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName");
                    return View(category);
                }
                var categoryexist = db.Categories.Where(m => m.SEOCategoryName == category.SEOCategoryName.Replace(" ", "-")).FirstOrDefault();
                if (categoryexist != null)
                {
                    ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName");
                    ModelState.AddModelError("", "sub category url already exist. please enter unique url");
                    return View(category);
                }
                if (categoryimage != null && categoryimage.ContentLength > 0)
                {
                    CategoryType categorytype = db.CategoryTypes.Find(category.CategoryTypeId);
                    string getimageExtention = Path.GetExtension(categoryimage.FileName);
                    Random rnumber = new Random();
                    string FileCategoryImage = Path.GetFileName("ci" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                    categoryimage.SaveAs(Server.MapPath("~/Content/Images/CategoryImages/") + FileCategoryImage);
                    string imagepath = @"https://24wallpapers.com/Content/Images/CategoryImages/" + FileCategoryImage;
                    //string imagepath = @"~/Content/Images/CategoryImages/" + FileCategoryImage;
                    category.CategoryImagePath = imagepath;
                    category.CategoryTypeName = categorytype.CategoryTypeName;
                    category.SEOCategoryName = category.SEOCategoryName.Replace(" ", "-");
                    category.SEOCategoryTypeName = categorytype.SEOCategoryTypeName.Replace(" ", "-");

                    category.CreatedOn = DateTime.Now;
                    category.UpdatedOn = null;
                    category.Review = 0;

                    db.Categories.Add(category);
                    db.SaveChanges();
                    ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName", category.CategoryTypeId);
                    ModelState.Clear();
                    ViewBag.SM = "caategory add successfully";
                    return View();
                }
                else
                {
                    ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName");
                    return View(category);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult EditCategory(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName");
            return View(category);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCategory(Category category, HttpPostedFileBase categoryimage)
        {

            if (category.CategoryName == "" || category.CategoryName == null ||
                category.MetaCatTitle == "" || category.MetaCatTitle == null ||
                category.SEOCategoryName == "" || category.SEOCategoryName == null ||
                category.MetaCatDescription == "" || category.MetaCatDescription == null)
            {
                ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName");
                return View(category);
            }
            CategoryType categorytype = db.CategoryTypes.Find(category.CategoryTypeId);
            if (categorytype == null)
            {
                ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName");
                return View(category);
            }
            if (categoryimage != null && categoryimage.ContentLength > 0)
            {
                string getimageExtention = Path.GetExtension(categoryimage.FileName);
                Random rnumber = new Random();
                string FileCategoryImage = Path.GetFileName("ci" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                categoryimage.SaveAs(Server.MapPath("~/Content/Images/CategoryImages/") + FileCategoryImage);
                string imagepath = @"https://24wallpapers.com/Content/Images/CategoryImages/" + FileCategoryImage;
                //string imagepath = @"~/Content/Images/CategoryImages/" + FileCategoryImage;
                category.CategoryImagePath = imagepath;
            }
            category.CategoryTypeName = categorytype.CategoryTypeName;
            category.SEOCategoryName = category.SEOCategoryName.Replace(" ", "-");
            category.SEOCategoryTypeName = categorytype.SEOCategoryTypeName.Replace(" ", "-");
            category.UpdatedOn = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName", category.CategoryTypeId);

            int i = objCrudCategoryBAL.UpdateCategoryById(category.CategoryId, category.CategoryName, category.SEOCategoryName);
            if (i > 0)
            {
                ViewBag.SM = "updated";
            }

            Category categorys = db.Categories.Find(category.CategoryId);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryTypeId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName");
            ViewBag.SM = "caategory edit successfully";
            return View(categorys);

        }



        [HttpPost]
        public JsonResult DeleteCategory(long? id)
        {
            int i = 0;
            Category category = db.Categories.Where(m => m.CategoryId == id).FirstOrDefault();
            if (category != null)
            {

                db.Categories.Remove(category);
                i = db.SaveChanges();
            }
            int k = objCrudCategoryBAL.DeleteCategoryById(id);
            if (k > 0)
            {
                ViewBag.SM = "deleted";
            }
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Wallpaper
        public ActionResult Wallpaper()
        {
            return View(db.Wallpapers.OrderByDescending(m => m.WallpaperId).ToList());
        }
        public ActionResult AddNewWallpaper()
        {
            MainCategoryBind();
            return View();
        }
        [HttpPost]
        public ActionResult AddNewWallpaper(Wallpaper wallpaper, HttpPostedFileBase[] wallpaperimage)
        {
            try
            {
                if (wallpaper.MetaWallTitle == "" || wallpaper.MetaWallTitle == null ||
                    wallpaper.MetaWallDescriptions == "" || wallpaper.MetaWallDescriptions == null)
                {
                    MainCategoryBind();
                    return View(wallpaper);
                }
                else if (wallpaperimage != null)
                {
                    if (Request.Files.Count > 11)
                    {
                        MainCategoryBind();
                        ModelState.AddModelError("", "Please select maximum 10 images");
                        return View();
                    }
                    foreach (HttpPostedFileBase file in wallpaperimage)
                    {
                        CategoryType cattype = db.CategoryTypes.Find(wallpaper.MainCategoryId);
                        Category cat = db.Categories.Find(wallpaper.CatId);

                        string getimageExtention = Path.GetExtension(file.FileName);
                        Random rnumber = new Random();
                        string FileWallpaperImage = Path.GetFileName("wi" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                        file.SaveAs(Server.MapPath("~/Content/Images/WallpaperImages/") + FileWallpaperImage);
                        string imagepath = @"https://24wallpapers.com/Content/Images/WallpaperImages/" + FileWallpaperImage;
                        //string imagepath = @"~/Content/Images/WallpaperImages/" + FileWallpaperImage;
                        wallpaper.WallpaperImagePath = imagepath;

                        wallpaper.MainCategoryName = cattype.CategoryTypeName;
                        wallpaper.CatName = cat.CategoryName;

                        wallpaper.CreatedOn = DateTime.Now;
                        wallpaper.UpdatedOn = null;
                        Random r = new Random();
                        wallpaper.SEOMainCategoryName = cattype.SEOCategoryTypeName.Replace(" ", "-");
                        wallpaper.SEOCatName = cat.SEOCategoryName.Replace(" ", "-");

                        wallpaper.SEOWallpaperName = "wi" + "-" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + r.Next().ToString();
                        db.Wallpapers.Add(wallpaper);
                        db.SaveChanges();
                    }

                    MainCategoryBind();
                    ViewBag.SM = "wallpaper add successfully";
                    return View();
                }
                else
                {
                    MainCategoryBind();
                    return View(wallpaper);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Ex = ex.ToString();
                MainCategoryBind();
                return View(wallpaper);
            }
        }
        public ActionResult EditWallpaper(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wallpaper wallpaper = db.Wallpapers.Find(id);
            if (wallpaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainCategoryId = new SelectList(db.CategoryTypes, "CategoryTypeId", "CategoryTypeName", wallpaper.MainCategoryId);
            ViewBag.CatId = new SelectList(db.Categories, "CategoryId", "CategoryName", wallpaper.CatId);

            //MainCategoryBind();
            return View(wallpaper);
        }
        [HttpPost]
        public ActionResult EditWallpaper(Wallpaper wallpaper, HttpPostedFileBase wallpaperimage)
        {
            if (wallpaper.MetaWallTitle == "" || wallpaper.MetaWallTitle == null ||
                wallpaper.MetaWallDescriptions == "" || wallpaper.MetaWallDescriptions == null
                )
            {
                MainCategoryBind();
                return View(wallpaper);
            }
            else if (wallpaperimage != null && wallpaperimage.ContentLength > 0)
            {
                CategoryType cattype = db.CategoryTypes.Find(wallpaper.MainCategoryId);
                Category cat = db.Categories.Find(wallpaper.CatId);

                string getimageExtention = Path.GetExtension(wallpaperimage.FileName);
                Random rnumber = new Random();
                string FileWallpaperImage = Path.GetFileName("wi" + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid() + rnumber.Next().ToString() + getimageExtention);
                wallpaperimage.SaveAs(Server.MapPath("~/Content/Images/WallpaperImages/") + FileWallpaperImage);
                string imagepath = @"https://24wallpapers.com/Content/Images/WallpaperImages/" + FileWallpaperImage;
                //string imagepath = @"~/Content/Images/WallpaperImages/" + FileWallpaperImage;
                wallpaper.WallpaperImagePath = imagepath;
                wallpaper.UpdatedOn = DateTime.Now;
                wallpaper.MainCategoryName = cattype.CategoryTypeName;
                wallpaper.CatName = cat.CategoryName;
                wallpaper.SEOMainCategoryName = cattype.SEOCategoryTypeName.Replace(" ", "-");
                wallpaper.SEOCatName = cat.SEOCategoryName.Replace(" ", "-");
                Random r = new Random();
                db.Entry(wallpaper).State = EntityState.Modified;
                db.SaveChanges();
                MainCategoryBind();
                ModelState.Clear();
                ViewBag.SM = "wallpaper add successfully";
                return View();
            }
            else
            {
                MainCategoryBind();
                return View(wallpaper);
            }
        }

        //Single Wallpaper Deleted
        [HttpPost]
        public JsonResult DeleteWallpaper(long? id)
        {
            int i = 0;
            Wallpaper wallpaper = db.Wallpapers.Where(m => m.WallpaperId == id).FirstOrDefault();
            if (wallpaper != null)
            {

                db.Wallpapers.Remove(wallpaper);
                i = db.SaveChanges();
            }
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        //Multiple Wallpaper Deleted
        [HttpPost]
        public ActionResult Wallpaper(FormCollection formCollection)
        {
            if (formCollection["ID"] == null)
            {
                ModelState.AddModelError("", "Please select wallpaper");
                ViewBag.SM = "Please select wallpaper";
                return View(db.Wallpapers.OrderByDescending(m => m.WallpaperId).ToList());
            }
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var employee = this.db.Wallpapers.Find(int.Parse(id));
                this.db.Wallpapers.Remove(employee);
                this.db.SaveChanges();
            }
            ViewBag.SMW = "wallpaper deleted";
            return View(db.Wallpapers.OrderByDescending(m => m.WallpaperId).ToList());
        }
        #endregion

        #region Support
        public ActionResult CustomerQuery()
        {
            return View(db.ContactEnquiries.OrderByDescending(m => m.ContactEnquiryId).ToList());
        }
        #endregion


        #region Binddata
        public void MainCategoryBind()
        {
            ViewBag.CatId = new SelectList(db.CategoryTypes.ToList(), "CategoryTypeId", "CategoryTypeName");
        }
        public JsonResult SubCategoryBind(long? CategoryTypeId)
        {
            var subcategory = new SelectList(db.Categories.Where(m => m.CategoryTypeId == CategoryTypeId).ToList(), "CategoryId", "CategoryName");
            return Json(subcategory, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult Faqs()
        {
            var getWallFaq = db.WallFaqs.OrderByDescending(m=>m.FaqId).ToList();
            return View(getWallFaq);
        }
        public ActionResult AddNewFaq()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewFaq(WallFaq obj)
        {
            obj.AutoGFaqId = RandomString(4);
            obj.CreatedOn = DateTime.Now;
            obj.UpdatedOn = null;
            db.WallFaqs.Add(obj);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.SM = "add";
            return View();
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random rnumber = new Random();

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)rnumber.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public ActionResult EditFaq(long? id)
        {
            if(id==null || id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WallFaq obj = db.WallFaqs.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFaq(WallFaq obj)
        {
            obj.UpdatedOn = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.SM = "edit";
            return View();
        }
    }
}