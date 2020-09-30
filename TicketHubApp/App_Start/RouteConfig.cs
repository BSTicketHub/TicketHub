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
                name: "SearchShop",
                url: "Home/Search/Shop/{input}",
                defaults: new { controller = "Customer", action = "ShopList", input = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "SearchTicket",
                url: "Home/Search/Ticket/{input}",
                defaults: new { controller = "Customer", action = "TicketList", input = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ProductCart",
                url: "Home/Cart",
                defaults: new { controller = "ProductCart", action = "ProductCart" }
            );
            routes.MapRoute(
                name: "ProductDescription",
                url: "Home/Product/{id}",
                defaults: new { controller = "ProductDescription", action = "Index"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
