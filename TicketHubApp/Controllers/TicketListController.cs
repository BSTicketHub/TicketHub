using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.ViewModels;

namespace TicketHubApp.Controllers
{
    public class TicketListController : Controller
    {
        // GET: TicketList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TicketList()
        {
            List<StoreTicketListViewModel> tickets = new List<StoreTicketListViewModel>
            {
                new StoreTicketListViewModel(){Id=Guid.NewGuid(), DeliveredDate = DateTime.Now,
                    Exchanged=true, ExchangedDate= DateTime.Now, Voided=true, VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(), UserId=Guid.NewGuid(), OrderId=Guid.NewGuid()},
                new StoreTicketListViewModel(){Id=Guid.NewGuid(), DeliveredDate = DateTime.Now,
                    Exchanged=true, ExchangedDate= DateTime.Now, Voided=true, VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(), UserId=Guid.NewGuid(), OrderId=Guid.NewGuid()},
            };



            return View(tickets);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}