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
            get
            {
                if (_signInManager == null)
                {
                    return HttpContext.GetOwinContext().Get<AppIdentitySignInManager>();
                }
                return _signInManager;
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AppIdentityUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    return HttpContext.GetOwinContext().Get<AppIdentityUserManager>();
                }
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
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
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserLogin(LoginViewModel viewModel, string returnUrl)
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
                    return await SignIn(viewModel, returnUrl);
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
            return await SignIn(viewModel, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShopLoginAsync(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            return await SignIn(viewModel, returnUrl);
        }

        [HttpGet]
        public ActionResult LoginPlatform()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPlatformAsync(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            return await SignIn(viewModel, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private async Task<ActionResult> SignIn(LoginViewModel viewModel, string returnUrl)
        {
            var signInResult = await SignInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
            switch (signInResult)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = viewModel.RememberMe });
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }

}
