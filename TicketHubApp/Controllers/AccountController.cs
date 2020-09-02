using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;

namespace TicketHubApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopLogin(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult LoginPlatform()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPlatform(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("LoginPlatform");
        }
    }

}
