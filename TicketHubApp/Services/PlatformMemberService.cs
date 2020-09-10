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
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);

            IQueryable<TicketHubUser> memberList = repository.GetAll();
            List<PlatformMemberViewModel> members = new List<PlatformMemberViewModel>(memberList.Count());

            foreach (var item in memberList)
            {
                var member = new PlatformMemberViewModel
                {
                    Id = item.Id,
                    UserAccount = item.UserName,
                    PasswordHash = item.PasswordHash,
                    Mobile = item.PhoneNumber,
                };
                members.Add(member);
            }
            return members;
        }
    }
}