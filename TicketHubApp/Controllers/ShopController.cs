using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

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

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult CreateProduct()
        {
            var categoryList = new List<SelectListItem>()
            {
                new SelectListItem{Text = "台式", Value= "category-1"},
                new SelectListItem{Text = "日式", Value= "category-2"},
                new SelectListItem{Text = "韓式", Value= "category-3"},
                new SelectListItem{Text = "中式", Value= "category-4"},
                new SelectListItem{Text = "美式", Value= "category-5"},
                new SelectListItem{Text = "泰式", Value= "category-6"},
                new SelectListItem{Text = "西式", Value= "category-7"},
                new SelectListItem{Text = "法式", Value= "category-8"},
                new SelectListItem{Text = "印度料理", Value= "category-9"},
                new SelectListItem{Text = "越南料理", Value= "category-10"}
            };
            categoryList.Where(q => q.Value == "category-1").First().Selected = true;

            ViewBag.CategoryList = categoryList;

            return View();
        }

        public ActionResult StoreInformation()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderDetails()
        {
            return View();
        }

        public ActionResult TicketDetails()
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