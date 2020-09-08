using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.PlatformViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformShopController : Controller
    {
        private TicketHubContext db = new TicketHubContext();

        // GET: ShopViewModels
        public ActionResult Index()
        {
            return View();
        }

    }
}