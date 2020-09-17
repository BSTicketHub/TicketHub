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
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

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
            return View();
        }

        //for api
        public ActionResult getIssueApi(int order, bool closed)
        {
            var service = new ShopIssueService();
            var viewModel = service.GetIssueListApi(order, closed).Items;
            string result = JsonConvert.SerializeObject(viewModel);

            return Json(result, JsonRequestBehavior.AllowGet);
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
                var result = service.CreateIssue(shopissueVM);
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
                var result = service.UpdateIssue(shopissueVM);
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
            ShopInfoService service = new ShopInfoService();
            ShopViewModel shopVM = service.GetShopInfo();
            if (shopVM == null)
            {
                return HttpNotFound();
            }

            var countryList = new TagService().GenCountry();
            ViewBag.countryList = countryList;

            TempData["ImgPath"] = shopVM.BannerImg;
            return View(shopVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopInfo(ShopViewModel shopVM)
        {
            var countryList = new TagService().GenCountry();
            ViewBag.countryList = countryList;
            ViewBag.Message = "";
            if (TempData["ImgPath"] != null)
            {
                shopVM.BannerImg = (string)TempData["ImgPath"];
            }

            if (ModelState.IsValid)
            {
                var service = new ShopInfoService();
                var result = service.UpdateShopInfo(shopVM);
                if (result.Success)
                {
                    return RedirectToAction("ShopInfo");
                }
                else
                {
                    ViewBag.Message = "更新失敗";
                    return View(shopVM);
                }
            }
            return View();
        }

        public ActionResult SalesReport()
        {
            return View();
        }

        public ActionResult getReportApi(List<string> duration)
        {
            var service = new ShopReportService();
            var result = service.getSalesRepoet(duration);

            return Json(result, JsonRequestBehavior.AllowGet);
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

        public ActionResult IssueDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = new ShopIssueService();
            var result = service.GetIssue(Guid.Parse(id));
            if (result == null)
            {
                return HttpNotFound();
            }

            ViewBag.IssueId = result.Id.ToString();
            return View(result);
        }

        public ActionResult getIssueDetailApi(string Id)
        {
            var service = new ShopIssueService();
            var jsonData = service.GetIssueDetailsApi(Guid.Parse(Id));
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}