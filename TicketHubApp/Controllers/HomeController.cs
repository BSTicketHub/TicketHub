using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new HomeCard();
            var CardVM = service.GetSortNewCard().Items;
            return View(CardVM);
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
    }
}