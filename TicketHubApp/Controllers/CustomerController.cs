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
            var currentUserId = User.Identity.GetUserId();
            var wishIssue = _context.UserWishIssue.Where(x => x.UserId == currentUserId).Select(x => x.IssueId).ToList();
            ViewBag.UserId = currentUserId;
            ViewBag.WishIssue = wishIssue;
            var tickets = _context.Issue.Select(x => new StoreTicketListViewModel
            {
                Id = x.Id,
                Memo = x.Memo,
                Title = x.Title,
                DiscountPrice = x.DiscountPrice,
                DiscountRatio = x.DiscountRatio,
                OriginalPrice = x.OriginalPrice,
                ImgPath = x.ImgPath
            });

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
                                   where ufs.UserId == user.Id
                                   select new ShopViewModel
                                   {
                                       Id = s.Id,
                                       ShopName = s.ShopName,
                                       ShopIntro = s.ShopIntro,
                                       City = s.City,
                                       District = s.District,
                                       Address = s.Address,
                                       Phone = s.Phone
                                   },
                    WishIssue = from i in _context.Issue
                                join uwi in _context.UserWishIssue on i.Id equals uwi.IssueId
                                where uwi.UserId == user.Id
                                select new ShopIssueViewModel
                                {
                                    DiscountPrice = i.DiscountPrice,
                                    Id = i.Id,
                                    Memo = i.Memo,
                                    Title = i.Title,
                                    OriginalPrice = i.OriginalPrice,
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

        [HttpPost]
        public ActionResult ToggleFavoriteList(string IssueId, string UserId)
        {

            using (var _context = TicketHubContext.Create())
            {
                var IssueGUID = Guid.Parse(IssueId);
                var entity = (from x in _context.UserWishIssue
                              where x.IssueId == IssueGUID && x.UserId == UserId
                              select x).FirstOrDefault();

                if (entity != null)
                {
                    _context.UserWishIssue.Remove(entity);

                }
                else
                {
                    _context.UserWishIssue.Add(new UserWishIssue
                    {
                        IssueId = Guid.Parse(IssueId),
                        UserId = UserId,
                        AddedDate = DateTime.Now
                    });

                }

                _context.SaveChanges();
            }
            return Content("Complete");
        }
    }
}