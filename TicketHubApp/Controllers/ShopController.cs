using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;
using TicketHubApp.Services;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace TicketHubApp.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private TicketHubContext _context = new TicketHubContext();

        // GET: Store
        public ActionResult Index()
        {
            return View("SalesReport");
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

        public ActionResult closeIssueApi(string Id)
        {
            var service = new ShopIssueService();
            var result = service.closeIssueAPi(Guid.Parse(Id));
            return Json(result.Success, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult ShopInfo()
        {
            ShopInfoService service = new ShopInfoService();
            ShopViewModel shopVM = service.GetShopInfo();
            if (shopVM == null)
            {
                return View();
            }

            TempData["ImgPath"] = shopVM.BannerImg;
            TempData["City"] = shopVM.City;
            TempData["Dinstrict"] = shopVM.District;
            return View(shopVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopInfo(ShopViewModel shopVM)
        {
            if (TempData["ImgPath"] != null)
            {
                shopVM.BannerImg = (string)TempData["ImgPath"];
            }

            if (ModelState.IsValid)
            {
                var service = new ShopInfoService();
                var coordinates = service.geocodeLatLng(shopVM.Address);
                var result = service.UpdateShopInfo(shopVM, coordinates);
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
            return View(shopVM);
        }

        public ActionResult SalesReport()
        {
            return View();
        }

        public ActionResult getReportApi(List<string> duration)
        {
            var service = new ShopReportService();
            var result = service.getSalesReport(duration);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopIssueApi()
        {
            var service = new ShopReportService();
            var result = service.getTopIssue();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopCustomerApi()
        {
            var service = new ShopReportService();
            var result = service.getTopCutsom();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult chartApi(List<string> Labels, int Type)
        {
            var service = new ShopReportService();
            var result = service.getChartCustomer(Labels, Type);

            return Json(result, JsonRequestBehavior.AllowGet);
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

        public ActionResult EmployeeList()
        {
            var service = new ShopEmployeeService();
            var result = service.GetEmployeeList();
            return View(result);
        }

        public ActionResult deleteEmployeeApi(string id)
        {
            AppIdentityUserManager userManeger = HttpContext.GetOwinContext().Get<AppIdentityUserManager>();

            var service = new ShopEmployeeService();
            var result = service.DeleteEmployee(id, userManeger);

            return RedirectToAction("EmployeeList");
        }

        [HttpGet]
        public ActionResult EmployeeCreate()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EmployeeCreate(string account)
        {
            var userManager = HttpContext.GetOwinContext().Get<AppIdentityUserManager>();
            var service = new ShopEmployeeService();
            var result = service.createEmployee(account, userManager);
            if (result.Success)
            {
                return RedirectToAction("EmployeeList");
            }

            return View();
        }

        public ActionResult searchAccountApi(string account)
        {
            var userManager = HttpContext.GetOwinContext().Get<AppIdentityUserManager>();
            var user = userManager.FindByEmail(account);
            if (user == null)
            {
                return Json("Email 不存在", JsonRequestBehavior.AllowGet);
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}