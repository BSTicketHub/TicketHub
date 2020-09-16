using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformShopService
    {   
        public DataTableViewModel GetShopsTableData()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            IQueryable<Shop> shopList = repository.GetAll();
            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach(var item in shopList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.ShopName);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);
                dataInstance.Add(item.AppliedDate.ToString());
                dataInstance.Add(item.ModifiedDate.ToString());

                table.data.Add(dataInstance);
            }
            return table;
        }

        public PlatformShopViewModel GetShop(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            var shop = repository.GetAll().FirstOrDefault(s => s.Id.ToString() == id);

            var shopVM = new PlatformShopViewModel()
            {
                Id = shop.Id,
                Address = shop.Address,
                BannerImg = shop.BannerImg,
                ShopName = shop.ShopName,
                Email = shop.Email,
                Fax = shop.Fax,
                Phone = shop.Phone,
                Website = shop.Website
            };

            return shopVM;
        }
        public DataTableViewModel GetEmployeesTableData(string id)
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<ShopEmployee> employeeRepository = new GenericRepository<ShopEmployee>(context);
            GenericRepository<TicketHubUser> userRepository = new GenericRepository<TicketHubUser>(context);

            var employeesList = from e in employeeRepository.GetAll().Where(e => e.ShopId.ToString() == id)
                                join u in userRepository.GetAll()
                                on e.UserId equals u.Id
                                select new PlatformEmployeeViewModel
                                {
                                    Id = u.Id,
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    Phone = u.PhoneNumber
                                    
                                };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in employeesList)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id);
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.Email);
                dataInstance.Add(item.Phone);

                table.data.Add(dataInstance);
            }

            return table;
        }
    }
}