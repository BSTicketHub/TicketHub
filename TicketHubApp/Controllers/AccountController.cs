using System.Web.Mvc;

namespace TicketHubApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginPlatform()
        {
            return View();
        }
    }
}
