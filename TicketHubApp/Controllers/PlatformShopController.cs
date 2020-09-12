using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.PlatformViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformShopController : Controller
    {
 
        // GET: Shops
        public ActionResult Index()
        {
            return View();             
        }

        public ActionResult GetShopsJson()
        {
            PlatformShopService service = new PlatformShopService();
            var shopsTableData = service.GetShopsTableData();

            return Json(shopsTableData, JsonRequestBehavior.AllowGet);
        }
    }
}