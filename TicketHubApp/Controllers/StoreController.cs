using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.ViewModels;

namespace TicketHubApp.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        public ActionResult CreateProduct2()
        {
            return View();
        }

        public ActionResult StoreInformation()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderDetails()
        {
            return View();
        }
    }
}