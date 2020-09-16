using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class PlatformAdminController : Controller
    {
        
        public ActionResult Index()
        {   
            return View();
        }


        public ActionResult AdminDetail(string id)
        {
            PlatformAdminService service = new PlatformAdminService();
            var admin = service.GetAdmin(id);

            return View(admin);
        }

        // Get Json Action
        public ActionResult GetAdminsJson()
        {   
            PlatformAdminService service = new PlatformAdminService();
            var adminsTableData = service.GetAdminsTableData();

            return Json(adminsTableData, JsonRequestBehavior.AllowGet);
        }
    }
}