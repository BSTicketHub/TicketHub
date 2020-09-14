using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;
using TicketHubApp.Services;
using System;

namespace TicketHubApp.Controllers
{
    public class ShopController : Controller
    {
        private TicketHubContext _context = new TicketHubContext();
        // GET: ShopList
        public ActionResult ShopList()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var FavoriteShop = _context.UserFavoriteShop.Select((x) => x.ShopId).ToList();
                ViewBag.FavoriteShop = FavoriteShop;
                ViewBag.UserId = userId;
            }
            return View(_context.Shop.Select(x => new ShopViewModel
            {
                Id = x.Id,
                ShopName = x.ShopName,
                ShopIntro = x.ShopIntro,
                City = x.City,
                District = x.District,
                Address = x.Address,
                Phone = x.Phone,
                Issues = _context.Issue.Where(y => y.ShopId == x.Id).Select(y => new SimpleIssueViewModel
                {
                    Id = y.Id,
                    DiscountPrice = y.DiscountPrice,
                    Memo = y.Memo,
                    OriginalPrice = y.OriginalPrice,
                    Title = y.Title
                })
            })); ;
        }

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult IssueList(int? orderValue)
        {
            var service = new ShopIssueService();
            var viewModel = service.GetAll(orderValue).Items;
            
            return View(viewModel);
        }

        public ActionResult CreateIssue()
        {
            var categoryList = new TagService().GenCategory();
            ViewBag.CategoryList = categoryList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIssue(ShopIssueViewModel shopissueVM)
        {
            var categoryList = new TagService().GenCategory();
            ViewBag.CategoryList = categoryList;

            if (ModelState.IsValid)
            {
                var service = new ShopIssueService();
                var result = service.Create(shopissueVM);
                if (result.Success)
                {
                    return RedirectToAction("IssueList");
                }
                else
                {
                    ViewBag.Message = "新增失敗!";
                    System.Console.WriteLine(result.Message);
                    return View(shopissueVM);
                }
            }
            return View();
        }

        public ActionResult EditIssue(string id)
        {
            var categoryList = new TagService().GenCategory();
            ViewBag.CategoryList = categoryList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = new ShopIssueService();
            ShopIssueViewModel shopissueVM = service.GetIssue(Guid.Parse(id));
            if (shopissueVM == null)
            {
                return HttpNotFound();
            }

            TempData["ImgPath"] = shopissueVM.ImgPath;
            return View(shopissueVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIssue(ShopIssueViewModel shopissueVM)
        {
            var categoryList = new TagService().GenCategory();
            ViewBag.CategoryList = categoryList;

            if (TempData["ImgPath"] != null)
            {
                shopissueVM.ImgPath = (string)TempData["ImgPath"];
            }

            if (ModelState.IsValid)
            {
                var service = new ShopIssueService();
                var result = service.Update(shopissueVM);
                if (result.Success)
                {
                    return RedirectToAction("IssueList");
                }
                else
                {
                    ViewBag.Message = "更新失敗!";
                    System.Console.WriteLine(result.Message);
                    return View(shopissueVM);
                }
            }
            return View();
        }

        public ActionResult ShopInfo()
        {
            return View();
        }

        public ActionResult IssueDetails()
        {
            return View();
        }

        public ActionResult SalesReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToFavoriteList(string ShopId, string UserId)
        {
            using(var _context = TicketHubContext.Create())
            {
                _context.UserFavoriteShop.Add(new UserFavoriteShop
                {
                    ShopId = Guid.Parse(ShopId),
                    UserId = UserId,
                    AddedDate = DateTime.Now
                });
                _context.SaveChanges();
            }
            return Json(ShopId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFromFavoriteList(string ShopId, string UserId)
        {
            using (var _context = TicketHubContext.Create())
            {
                var ShopGUID = Guid.Parse(ShopId);
                var entity = (from x in _context.UserFavoriteShop
                             where x.ShopId == ShopGUID && x.UserId == UserId
                             select x).FirstOrDefault();
                _context.UserFavoriteShop.Remove(entity);
                _context.SaveChanges();
            }
            return Content("Deleted");
        }
    }
}