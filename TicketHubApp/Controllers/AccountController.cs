using System.Web.Mvc;
using TicketHubApp.Interfaces;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (TempData["CustErrMsg"] != null)
            {
                ViewBag.CustErrMsg = TempData["CustErrMsg"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                if (viewModel.IsSignUp)
                {
                    IResult resultCreate = accountService.CreateUser(viewModel);
                    if (!resultCreate.Success)
                    {
                        TempData["CustErrMsg"] = resultCreate.Message;
                        return RedirectToAction("Login");
                    }
                }

                IResult resultValidate = accountService.ValidateUser(viewModel);
                if (!resultValidate.Success)
                {
                    TempData["CustErrMsg"] = resultValidate.Message;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopLogin(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();

                IResult resultValidate = accountService.ValidateShopUser(viewModel);
                if (!resultValidate.Success)
                {
                    TempData["ShopErrMsg"] = resultValidate.Message;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login");
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
