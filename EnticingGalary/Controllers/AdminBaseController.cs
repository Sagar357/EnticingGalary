using System.Web.Mvc;

namespace EnticingGalary.Controllers
{
    public class AdminBaseController : Controller
    {
        // GET: AdminBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["AdminLoginId"] == null && Session["EmailId"] == null && Session["IsActive"] == null)
            {
                filterContext.Result = RedirectToAction("Index", "AdminLogin");
            }
        }
    }
}