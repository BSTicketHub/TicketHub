using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformMemberController : Controller
    { 
        // GET: Users
        public ActionResult Index()
        {
            PlatformMemberService service = new PlatformMemberService();
            var members = service.GetAllMembers();
            return View(members);
        }

        public ActionResult CreateMember()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember([Bind(Include ="Id, UserName, Mobile, Email")] PlatformMemberViewModel memberVM)
        {   
            return View();
        }

        public ActionResult MemberDetail(int? id)
        {
            return View();
        }

        public ActionResult EditMember(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditMember([Bind(Include = "Id, UserName, Mobile, Email")] PlatformMemberViewModel memberVM)
        {
            return View();
        }

        public ActionResult DeleteMember(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteMember(int id)
        {
            return View();
        }

    }
}
