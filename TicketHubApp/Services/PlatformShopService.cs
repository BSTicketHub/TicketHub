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
        public List<PlatformShopViewModel> GetAllShops()
        {
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Shop> repository = new GenericRepository<Shop>(context);
            IQueryable<Shop> shopList = repository.GetAll();

            List<PlatformShopViewModel> shops = new List<PlatformShopViewModel>(shopList.Count());

            foreach (var item in shopList)
            {
                var shop = new PlatformShopViewModel
                {
                    Id = item.Id,
                    ShopName = item.ShopName,
                    Phone = item.Phone,
                    Fax = item.Fax,
                    Address = item.Address,
                    Email = item.Email,
                    Website = item.Website
                };
                shops.Add(shop);
            }

            return shops;
        }
    }
}