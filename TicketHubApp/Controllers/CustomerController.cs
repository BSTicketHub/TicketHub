using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class CustomerController : Controller
    {
        private TicketHubContext _context = new TicketHubContext();
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

        public ActionResult GetCustomerInfo()
        {
            var userId = "91142d0f-9681-4b41-86d6-8a583b52bc98";
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            var info = new CustomerInfoViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
    }
}