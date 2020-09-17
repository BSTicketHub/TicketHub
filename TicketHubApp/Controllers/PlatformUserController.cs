using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformUserController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult UserDetail(string id)
        {
            PlatformUserService service = new PlatformUserService();
            var user = service.GetUser(id);

            return View(user);
        }

        public ActionResult GetTicketsBelongsToThisUser(string id)
        {
            ViewBag.id = id;
            return View();
        }

        // Get Json Action
        public ActionResult GetUsersJson()
        {
            PlatformUserService service = new PlatformUserService();
            var usersTableData = service.GetUsersTableData();

            return Json(usersTableData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTicketsByUserJson(string id)
        {
            PlatformUserService service = new PlatformUserService();
            var ticketsTableData = service.GetTicketsTableData(id);

            return Json(ticketsTableData, JsonRequestBehavior.AllowGet);
        }

    }
}
