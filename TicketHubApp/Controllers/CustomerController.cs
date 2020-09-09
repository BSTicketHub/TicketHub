using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;

namespace TicketHubApp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerDetail
        public ActionResult CustomerPage()
        {
            var viewModel = new List<CustomerDetailViewModel>()
            {
                new CustomerDetailViewModel{UserName = "Jack", Email = "abc@123", Mobile = "0963157894" }
            };
            return View(viewModel);
        }

        public ActionResult TicketList()
        {
            List<StoreTicketListViewModel> tickets = new List<StoreTicketListViewModel>
            {
                new StoreTicketListViewModel(){
                    Id=Guid.NewGuid(),
                    DeliveredDate = DateTime.Now,
                    Exchanged=true,
                    ExchangedDate= DateTime.Now,
                    Voided=true,
                    VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(),
                    UserId=Guid.NewGuid(),
                    OrderId=Guid.NewGuid()
                },
                new StoreTicketListViewModel(){
                    Id=Guid.NewGuid(),
                    DeliveredDate = DateTime.Now,
                    Exchanged=true,
                    ExchangedDate= DateTime.Now,
                    Voided=true,
                    VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(),
                    UserId=Guid.NewGuid(),
                    OrderId=Guid.NewGuid()},
            };

            return View(tickets);
        }
    }
}