using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketHubApp.Controllers
{
    public class ShopListController : Controller
    {
        private TicketHubContext _context = new TicketHubContext();
        // GET: ShopList
        public ActionResult ShopList()
        {
            return View();
        }
    }
}