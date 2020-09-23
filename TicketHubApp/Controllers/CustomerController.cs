using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class CustomerController : Controller
    {
        private TicketHubContext _context = TicketHubContext.Create();

        // GET: CustomerDetail
        [Authorize]
        public ActionResult CustomerInfo()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyTicket()
        {
            return View();
        }

        [Authorize]
        public ActionResult WishList()
        {
            return View();
        }

        [Authorize]
        public ActionResult FavoriteShop()
        {
            return View();
        }

        public ActionResult TicketList(string input)
        {
            var service = new TicketListService();
            if (User.Identity.IsAuthenticated)
            { 
                var currentUserId = User.Identity.GetUserId();
                var wishIssue = service.GetUserFsavotite(currentUserId);
                ViewBag.UserId = currentUserId;
                ViewBag.WishIssue = wishIssue;
            }
            var tickets = service.SearchIssue(input);

            if (tickets.Count() == 0)
            {
                return RedirectToRoute("Unfound");
            }
            else
            {
                return View(tickets);
            }
        }

        public ActionResult GetCustomerInfo()
        {
            var service = new CustomerInfoService();
            var currentUserId = User.Identity.GetUserId();
            var info = service.GetInfo(currentUserId);
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
            return Json(IssueId, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IssueTagSearch(IssueListSearchViewModel model)
        {
            model.SelectedTag = model.SelectedTag is null ? new List<string>() : model.SelectedTag;

            var service = new TicketListService();
            var tickets = service.SearchByTag(model.MaxPrice, model.MinPrice);
            if(model.SelectedTag.Count != 0)
            {
                tickets = tickets.Where(x => x.TagList.Intersect(model.SelectedTag).Count() > 0);
            }

            

            return Json(tickets, JsonRequestBehavior.AllowGet);
        }
    }
}