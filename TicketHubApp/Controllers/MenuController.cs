using System.Web.Mvc;

namespace TicketHubApp.Controllers
{
    public class MenuController : Controller
    {

        public ActionResult GenSideMenu(string page)
        {
            ViewBag.Page = page;
            return PartialView("_Navbar");
        }

    }
}
