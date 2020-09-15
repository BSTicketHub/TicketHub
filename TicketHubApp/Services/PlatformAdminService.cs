using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformAdminService
    {
        public DataTableViewModel GetAdminsTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> userRepository = new GenericRepository<TicketHubUser>(context);
            GenericRepository<IdentityUserRole> roleRepository = new GenericRepository<IdentityUserRole>(context);

            var adminList =  from u in userRepository.GetAll()
                             join r in roleRepository.GetAll()
                             on u.Id equals r.UserId
                             where r.RoleId == "1" || r.RoleId == "2"
                             select new PlatformAdminViewModel
                             {
                                Id = u.Id,
                                Name = u.UserName,
                                Email = u.Email,
                                Phone = u.PhoneNumber
                             };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in adminList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.Name);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);

                table.data.Add(dataInstance);
            }

            return table;
        }
        public PlatformAdminViewModel GetAdmin(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<TicketHubUser> repository = new GenericRepository<TicketHubUser>(context);
            var admin = repository.GetAll().FirstOrDefault(x => x.Id == id);

            var adminVM = new PlatformAdminViewModel
            {
                Id = admin.Id,
                Name = admin.UserName,
                Email = admin.Email,
                Phone = admin.PhoneNumber
            };

            return adminVM;
        }
    }
}