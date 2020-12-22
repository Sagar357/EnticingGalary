using System.Web;
using System.Web.Optimization;

namespace EnticingGalary
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Content/web/css").Include(
                        "~/Content/web/css/bootstarp.min.css"));
            bundles.Add(new ScriptBundle("~/Content/web/css").Include(
                                    "~/Content/web/css/style.js"));

            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/popper.min.js"));
            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/wow.js"));
            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/ImgEffect.js"));
            bundles.Add(new ScriptBundle("~/Content/web/js").Include(
                        "~/Content/web/js/scrollTop.js"));
        }
    }
}
