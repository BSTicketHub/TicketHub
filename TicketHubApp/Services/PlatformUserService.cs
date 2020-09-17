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

        public DataTableViewModel GetTicketsTableData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Ticket> ticketRepository = new GenericRepository<Ticket>(context);
            GenericRepository<Shop> shopRepository = new GenericRepository<Shop>(context);
            GenericRepository<Issue> issueRepository = new GenericRepository<Issue>(context);

            var ticketList = from t in ticketRepository.GetAll().Where(t => t.UserId == id)
                             join i in issueRepository.GetAll()
                             on t.IssueId equals i.Id
                             join s in shopRepository.GetAll()
                             on i.ShopId equals s.Id
                             select new PlatformUserTicketsViewModel
                             {
                                 Id = t.Id,
                                 Name = i.Title,
                                 Price = i.DiscountPrice,
                                 ShopName = s.ShopName,
                                 DeliveredDate = t.DeliveredDate,
                                 Exchanged = t.Exchanged,
                                 ExchangedDate = t.ExchangedDate,
                                 Voided = t.Voided,
                                 VoidedDate = t.VoidedDate
                             };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in ticketList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.Name);
                dataInstance.Add(item.Price.ToString());
                dataInstance.Add(item.ShopName);
                dataInstance.Add(item.DeliveredDate.ToLocalTime().ToString());
                dataInstance.Add(item.Exchanged.ToString());
                dataInstance.Add(item.ExchangedDate.ToString() ?? "NA");
                dataInstance.Add(item.Voided.ToString());
                dataInstance.Add(item.VoidedDate.ToString() ?? "NA");
               
                table.data.Add(dataInstance);
            }

            return table;
        }
    }
}