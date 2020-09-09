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
        public List<PlatformMemberViewModel> GetAllMembers()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<User> repository = new GenericRepository<User>(context);

            IQueryable<User> memberList = repository.GetAll();
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