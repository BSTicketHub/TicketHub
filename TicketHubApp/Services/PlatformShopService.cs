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
                dataInstance.Add(item.ModifiedDate.ToString("yyyy-MM-dd"));

                table.data.Add(dataInstance);
            }
            return table;
        }
    }
}