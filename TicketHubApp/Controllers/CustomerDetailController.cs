using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;

namespace TicketHubApp.Controllers
{
    public class CustomerDetailController : Controller
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
    }
}