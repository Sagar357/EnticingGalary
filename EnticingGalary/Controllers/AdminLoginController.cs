using EnticingGalary.DAL;
using EnticingGalary.Models;
using System.Linq;
using System.Web.Mvc;


namespace EnticingGalary.Controllers
{
    public class AdminLoginController : Controller
    {
        private EnticingWallpaperDbEntities db = new EnticingWallpaperDbEntities();
        Encrypt_Decrypt objEncryptDecrypt = new Encrypt_Decrypt();
        // GET: AdminLogin
        
        public ActionResult Index()
        {
            return View();
        }        
        [HttpPost()] 
        [ValidateAntiForgeryToken]
        public ActionResult Index(AdminLogin objAdminLogin)
        {
            if (ModelState.IsValid)
            {
                string encryptPwd = "";
                if (objAdminLogin.AdminPwd != null)
                {
                    encryptPwd = objEncryptDecrypt.Encrypt(objAdminLogin.AdminPwd.ToString());
                }
                var obj = db.AdminLogins.Where(a => a.EmailId.Equals(objAdminLogin.EmailId) && a.AdminPwd.Equals(encryptPwd.ToString())).FirstOrDefault();
                if (obj != null)
                {
                    Session["AdminLoginId"] = obj.AdminLoginId.ToString();
                    Session["EmailId"] = obj.EmailId.ToString();
                    Session["IsActive"] = obj.IsActive.ToString();
                    if(Session["IsActive"].ToString()=="blogger")
                    {

                        return RedirectToAction("Blogs", "Admin");
                    }
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {

                    ModelState.AddModelError("", "Invalid login attempt.");
                }

            }

            return View();
        }
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");
        }

    }
}