using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new HomeCardService();
            var homecard = new HomeListViewModel()
            {
                BestSellerItems = service.GetBestSellerCard(),
                RecommenItems = service.GetRecommenCard(),
                SortNewItems = service.GetSortNewCard()
            };

            return View(homecard);
        }

        public ActionResult PageUnfound()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchType, string SearchContent)
        {
            if(SearchType == "餐廳")
            {
                return RedirectToRoute("ShopList", new { input = SearchContent });
            } else if(SearchType == "票券")
            {
                return RedirectToRoute("TicketList", new { input = SearchContent });
            }
            return Content("");
        }
    }
}