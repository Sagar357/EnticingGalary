using EnticingGalary.App_Start;
using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnticingGalary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                        
            //sub category details
            //wallpaper list
            routes.Add("SubCategoryDetails", new GetSEOFriendlyRoute("wallpapers/{SEOCategoryName}/{PageNumber}",
            new RouteValueDictionary(new { controller = "Category", action = "SubCategoryDetail" }),
            new MvcRouteHandler()));

            //category details

            //Global Search Header
            routes.Add("CategoryDetail", new GetSEOFriendlyRoute("category/{SEOMainCategoryName}",
            new RouteValueDictionary(new { controller = "Category", action = "SubMainCategory" }),
            new MvcRouteHandler()));

            //sub category list
            routes.Add("CategoryDetails", new GetSEOFriendlyRoute("wallpaper/{SEOMainCategoryName}",
            new RouteValueDictionary(new { controller = "Category", action = "SubCategory" }),
            new MvcRouteHandler()));

            //all category
            routes.Add("CategoryAll", new GetSEOFriendlyRoute("categories",
            new RouteValueDictionary(new { controller = "Category", action = "Index" }),
            new MvcRouteHandler()));

            //faq
            routes.Add("Faq", new GetSEOFriendlyRoute("faqs",
            new RouteValueDictionary(new { controller = "Home", action = "Faq" }),
            new MvcRouteHandler()));

            //sitemap
            routes.Add("Sitemap", new GetSEOFriendlyRoute("sitemap",
            new RouteValueDictionary(new { controller = "Home", action = "Sitemap" }),
            new MvcRouteHandler()));


            //terms
            routes.Add("TermsConditions", new GetSEOFriendlyRoute("terms-and-conditions",
            new RouteValueDictionary(new { controller = "Home", action = "Termsandcondition" }),
            new MvcRouteHandler()));

            //privacy
            routes.Add("PrivacyPolicy", new GetSEOFriendlyRoute("privacy-policy",
            new RouteValueDictionary(new { controller = "Home", action = "Privacypolicy" }),
            new MvcRouteHandler()));

            //contact us
            routes.Add("ContactUs", new GetSEOFriendlyRoute("contact-us",
            new RouteValueDictionary(new { controller = "Home", action = "Contact" }),
            new MvcRouteHandler()));

            //about us
            routes.Add("AboutUs", new GetSEOFriendlyRoute("about-us",
            new RouteValueDictionary(new { controller = "Home", action = "About" }),
            new MvcRouteHandler()));

            //home
            routes.Add("Home", new GetSEOFriendlyRoute("home",
            new RouteValueDictionary(new { controller = "Home", action = "Index" }),
            new MvcRouteHandler()));

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
