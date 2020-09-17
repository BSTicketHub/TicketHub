using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketHubApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );
            routes.MapRoute(
                name: "ShopList",
                url: "ShopList/{action}",
                defaults: new { controller = "ShopList", action = "ShopList" }
            );
            routes.MapRoute(
                name: "MemberViewModels",
                url: "MemberViewModels/{action}",
                defaults: new { controller = "MemberViewModels", action = "Index"});

            routes.MapRoute(
                name: "Shop",
                url: "Shop/{action}",
                defaults: new { controller = "Shop", action = "HomePage" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductCart",
                url: "ProductCart/{action}",
                defaults: new { controller = "ProductCart", action = "Cart" }
            );
          
            routes.MapRoute(
                name: "ProductDescription",
                url: "ProductDescription/{action}",
                defaults: new { controller = "ProductDescription", action = "Index"}
            );
        }
    }
}
