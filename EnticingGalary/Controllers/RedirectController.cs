using EnticingGalary.BAL;
using EnticingGalary.DAL;
using EnticingGalary.Models;
using EnticingGalary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

/*
 * Author:Sagar Srivastava ,
 * Aim:A section on the admin page for redirection of links is to be added. So, that from the admin panel any source URL in our site can be redirected to any target URL on our website.
 * Created Date:29-10-2020,
 * Chnage Log:
 * code ,name ,description ,date
 * =============================================================================================================================================================================================
 */

namespace EnticingGalary.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();
        Encrypt_Decrypt objEncryptDecrypt = new Encrypt_Decrypt();
        AdminDAL objAdminDAL = new AdminDAL();
        CrudCategoryBAL objCrudCategoryBAL = new CrudCategoryBAL();
        public ActionResult Index(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            //ViewBag.RedirectList = db.Categories.Where(m=>!string.IsNullOrEmpty(m.alias)).ToList();
            Redirect_List redirect_List = new Redirect_List();
            redirect_List.List= db.Categories.Where(m => !string.IsNullOrEmpty(m.alias)).ToList<Category>();

            return View("Redirect" ,redirect_List);
        }

        // GET: Redirect/Details/5 
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Redirect/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Redirect/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Redirect/Edit/5
        public ActionResult Edit(int id)
        {
         
            return View();
        }

        // POST: Redirect/Edit/5
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit( FormCollection collection)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            try
            {
                // TODO: Add update logic here
                if(!collection.AllKeys.Contains("SourceCategoryId") || !collection.AllKeys.Contains("DestinationCategoryId"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int source= Convert.ToInt32(collection["SourceCategoryId"]);
                int destination= Convert.ToInt32(collection["DestinationCategoryId"]);
                var x=db.Categories.SingleOrDefault(m => m.CategoryId==source);
                var y = db.Categories.SingleOrDefault(m => m.CategoryId == destination);
                if (x != null && y!=null)
                {
                    x.UpdatedOn = DateTime.Now;
                    x.alias = y.CategoryName;
                    db.SaveChanges();
                    ViewBag.Message = "Updated";
                }
                return View("Redirect");
            }
            catch
            {
                return View("Redirect");
            }
        }

        // GET: Redirect/Delete/5
        public ActionResult Delete(int id)
        {
            var x = db.Categories.SingleOrDefault(m=>m.CategoryId==id);
            string Message = string.Empty;
            if (x != null)
            {
                x.UpdatedOn = DateTime.Now;
                x.alias = "";
                db.SaveChanges();
                Message = "Deleted";
            }
            return RedirectToAction("Index" ,"Redirect" ,new { message=Message });
        }

        // POST: Redirect/Delete/5
       
        public ActionResult EditLink(int id)
        {
           
                // TODO: Add delete logic here
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                
                //ViewBag.RedirectList = db.Categories.Where(m=>!string.IsNullOrEmpty(m.alias)).ToList();
                Redirect_List redirect_List = new Redirect_List();
                redirect_List.List = db.Categories.Where(m => !string.IsNullOrEmpty(m.alias)).ToList<Category>();
                var result = db.Categories.Where(m=>m.CategoryId==id).FirstOrDefault();
                ViewBag.source = result.CategoryName;
                ViewBag.destination = result.alias;
                return View("redirect" ,redirect_List);
         
        }
    }
}
