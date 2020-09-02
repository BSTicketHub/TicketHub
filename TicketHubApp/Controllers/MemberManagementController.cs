using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketHubApp.Controllers
{
    public class MemberManagementController : Controller
    {
        // GET: MemberManagement
        public ActionResult Index()
        {

            List<PlatformViewModels.MemberViewModel> memberList = new List<PlatformViewModels.MemberViewModel>()
            {
                new PlatformViewModels.MemberViewModel { Id = 001, Name = "Joe", Email = "x31207joe@gmail.com", Mobile = "0973387232"},
                new PlatformViewModels.MemberViewModel { Id = 002, Name = "Joe", Email = "x31207joe@gmail.com", Mobile = "0973387232"},
                new PlatformViewModels.MemberViewModel { Id = 003, Name = "Joe", Email = "x31207joe@gmail.com", Mobile = "0973387232"},
            };

            return View(memberList);
        }


    }
}