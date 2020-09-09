using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformMemberService
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

        public PlatformMemberService()
        {
        }
        public PlatformMemberService(DbContext context)
        {
            _context = context;
        }

        public List<PlatformMemberViewModel> GetAllMembers()
        {
            IQueryable<User> memberList = Repository.GetAll();
            List<PlatformMemberViewModel> members = new List<PlatformMemberViewModel>(memberList.Count());

            foreach (var item in memberList)
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
            return members;
        }
    }
}