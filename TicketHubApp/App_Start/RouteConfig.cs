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
                name: "CustomerDetail",
                url: "CustomerDetail/{action}",
                defaults: new { controller = "CustomerDetail", action = "CustomerPage" }
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
                name: "Store",
                url: "Store/{action}",
                defaults: new { controller = "Store", action = "HomePage" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
