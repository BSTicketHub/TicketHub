using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformUserService
    {
        public DataTableViewModel GetUsersTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            GenericRepository<IdentityUserRole> roleRepository = new GenericRepository<IdentityUserRole>(context);

            var memberList = from u in repository.GetAll()
                             join r in roleRepository.GetAll()
                             on u.Id equals r.UserId
                             where r.RoleId == "5"
                             select new PlatformUserViewModel
                             {
                                 Id = u.Id,
                                 UserAccount = u.UserName,
                                 Mobile = u.PhoneNumber,
                             };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in memberList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.UserAccount);
                dataInstance.Add(item.Mobile ?? "NA");

                table.data.Add(dataInstance);
            }

            return table;
        }


        public PlatformUserViewModel GetUser(string id)
        {   

            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var user = repository.GetAll().FirstOrDefault(x => x.Id == id);

            var userVM = new PlatformUserViewModel
            { 
                Id = user.Id,
                UserAccount = user.UserName,
                Mobile = user.PhoneNumber
            };

            return userVM;
        }

        public List<Ticket> GetTickets(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Ticket> repository = new GenericRepository<Ticket>(context);
            var tickets = repository.GetAll().Where(x => x.UserId == id);

            return tickets.ToList();
        }
    }
}