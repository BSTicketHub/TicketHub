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
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformShopController : Controller
    {

        public ActionResult Index()
        {
            return View();             
        }


        public ActionResult ShopDetail(string id)
        {
            PlatformShopService service = new PlatformShopService();
            var shop = service.GetShop(id);

            return View(shop);
        }
        public ActionResult GetAllEmployees(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetAllIssues(string id)
        {
            ViewBag.id = id;

            return View();
        }

        // Get JSON Data
        public ActionResult GetShopsJson()
        {
            PlatformShopService service = new PlatformShopService();
            var shopsTableData = service.GetShopsTableData();

            return Json(shopsTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeesByShopJson(string id)
        {
            PlatformShopService service = new PlatformShopService();
            var employeesTableData = service.GetEmployeesTableData(id);

            return Json(employeesTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIssuessByShopJson(string id)
        {
            PlatformShopService service = new PlatformShopService();
            var issueTableData = service.GetIssuesTableData(id);

            return Json(issueTableData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSalesDataByIssue(string id)
        {   

            PlatformShopService service = new PlatformShopService();
            var issueSalesData = service.GetSalesData(id);

            return Json(issueSalesData, JsonRequestBehavior.AllowGet);
        }

    }
}