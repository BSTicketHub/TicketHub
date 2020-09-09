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
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformMemberController : Controller
    {
        private DbContext _context;
        private IRepository<User> _userRepository;
        private IRepository<User> Repository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            IQueryable<User> memberList = Repository.GetAll();
            List<PlatformMemberViewModel> members = new List<PlatformMemberViewModel>(memberList.Count());           
            foreach(var item in memberList)
            {
                var member = new PlatformMemberViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Mobile = item.Mobile,
                    Email = item.Email,
                };
                members.Add(member);
            }

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
