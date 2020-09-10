using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

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
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        public AccountController()
        {
        }
        public AccountController(AppIdentityUserManager userManager, AppIdentitySignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

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
                var user = UserManager.FindByEmail(viewModel.Email);
                if (user != null)
                {
                    //登入
                    return await SignIn(viewModel);
                }

                var newUser = new TicketHubUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email
                };

                var createResult = await UserManager.CreateAsync(newUser, viewModel.Password);

                if (!createResult.Succeeded)
                {
                    AddErrors(createResult);
                    return View("Login");
                }
            }

            //登入
            return await SignIn(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShopLoginAsync(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            return await SignIn(viewModel);
        }

        [HttpGet]
        public ActionResult LoginPlatform()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPlatformAsync(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            return await SignIn(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private async Task<ActionResult> SignIn(LoginViewModel viewModel)
        {
            var signInResult = await SignInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
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
                    return View("Login", viewModel);
            }
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
