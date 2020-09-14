using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class CustomerController : Controller
    {
        private TicketHubContext _context = TicketHubContext.Create();
        // GET: CustomerDetail
        public ActionResult CustomerInfo()
        {
            return View();
        }

        public ActionResult MyTicket()
        {
            return View();
        }

        public ActionResult WishList()
        {
            return View();
        }

        public ActionResult FavoriteShop()
        {
            return View();
        }

        public ActionResult TicketList()
        {
            List<StoreTicketListViewModel> tickets = new List<StoreTicketListViewModel>
            {
                new StoreTicketListViewModel(){
                    Id=Guid.NewGuid(),
                    DeliveredDate = DateTime.Now,
                    Exchanged=true,
                    ExchangedDate= DateTime.Now,
                    Voided=true,
                    VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(),
                    UserId=Guid.NewGuid(),
                    OrderId=Guid.NewGuid()
                },
                new StoreTicketListViewModel(){
                    Id=Guid.NewGuid(),
                    DeliveredDate = DateTime.Now,
                    Exchanged=true,
                    ExchangedDate= DateTime.Now,
                    Voided=true,
                    VoidedDate=DateTime.Now,
                    IssueId=Guid.NewGuid(),
                    UserId=Guid.NewGuid(),
                    OrderId=Guid.NewGuid()},
            };

            return View(tickets);
        }

        public ActionResult GetCustomerInfo()
        {
                var currentUserId = User.Identity.GetUserId();
                var user = _context.Users.Find(currentUserId);
                var info = new CustomerInfoViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Sex = user.Sex,
                    FavoriteShop = from s in _context.Shop
                                   join ufs in _context.UserFavoriteShop on s.Id equals ufs.ShopId
                                   select new ShopViewModel
                                   {
                                       Id = s.Id,
                                       ShopName = s.ShopName,
                                       ShopIntro = s.ShopIntro,
                                       City = s.City,
                                       District = s.District,
                                       Address = s.Address,
                                       Phone = s.Phone
                                   }
                };
                return Json(info, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeInfoData(string Id, string UserName, string Sex, string Email, string PhoneNumber)
        {
            using (var _context = TicketHubContext.Create())
            {
                var user = _context.Users.Find(Id);
                user.UserName = UserName;
                user.Sex = Sex;
                user.Email = Email;
                user.PhoneNumber = PhoneNumber;

                _context.SaveChanges();

                var info = new CustomerInfoViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Sex = user.Sex
                };
                return Json(info, JsonRequestBehavior.AllowGet);
            }
        }
    }
}