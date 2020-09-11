using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class PlatformOrderController : Controller
    {
        // GET: PlatformOrder
        public ActionResult Index()
        {
            PlatformOrderService service = new PlatformOrderService();
            var orders = service.GetAllOrders();

            return View(orders);
        }
    }
}