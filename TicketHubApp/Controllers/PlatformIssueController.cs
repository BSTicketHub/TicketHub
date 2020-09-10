using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class PlatformIssueController : Controller
    {
        // GET: PlatformIssue
        public ActionResult Index()
        {
            PlatformIssueService service = new PlatformIssueService();
            var orders = service.GetAllIssues();

            return View(orders);
        }
    }
}