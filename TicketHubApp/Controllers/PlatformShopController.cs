using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.PlatformViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformShopController : Controller
    {
        private DbContext _context;
        private IRepository<Shop> _shopRepository;
        private IRepository<Shop> Repository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_shopRepository == null)
                {
                    _shopRepository = new GenericRepository<Shop>(_context);
                }
                return _shopRepository;
            }
        }
        // GET: Shops
        public ActionResult Index()
        {
            IQueryable<Shop> shopList = Repository.GetAll();

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

            return View(shops);
        }

    }
}