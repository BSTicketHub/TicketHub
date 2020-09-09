using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class AccountController : Controller
    {
        private AppIdentitySignInManager _signInManager;
        private AppIdentityUserManager _userManager;
        public AppIdentitySignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<AppIdentitySignInManager>();
            private set => _signInManager = value;
        }
        public AppIdentityUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().Get<AppIdentityUserManager>();
            private set => _userManager = value;
        }

        public AccountController(AppIdentityUserManager userManager, AppIdentitySignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (TempData["CustErrMsg"] != null)
            {
                ViewBag.CustErrMsg = TempData["CustErrMsg"];
            }
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UserLogin(LoginViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AccountService accountService = new AccountService();
        //        if (viewModel.IsSignUp)
        //        {
        //            IResult resultCreate = accountService.CreateUser(viewModel);
        //            if (!resultCreate.Success)
        //            {
        //                TempData["CustErrMsg"] = resultCreate.Message;
        //                return RedirectToAction("Login");
        //            }
        //        }

        //        IResult resultValidate = accountService.ValidateUser(viewModel);
        //        if (!resultValidate.Success)
        //        {
        //            TempData["CustErrMsg"] = resultValidate.Message;
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return RedirectToAction("Login");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserLogin(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            //註冊
            if (viewModel.IsSignUp)
            {
                var user = new TicketHubUser
                {
                    UserName = viewModel.Account,
                    Email = viewModel.Account
                };

                var createResult = await UserManager.CreateAsync(user, viewModel.Password);

                if (!createResult.Succeeded)
                {
                    AddErrors(createResult);
                    return RedirectToAction("Login");
                }
            }

            //登入
            var signInResult = await SignInManager.PasswordSignInAsync(viewModel.Account, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
            switch (signInResult)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { RememberMe = viewModel.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "登入嘗試失試。");
                    return View(viewModel);
            }
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }

}
