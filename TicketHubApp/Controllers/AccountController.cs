using System.IO;
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
                if (createResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(newUser.Id, RoleName.CUSTOMER);
                    await SendEmailConfirmationAsync(newUser);
                    return View("RegisterSuccess");
                }
                else
                {
                    AddErrors(createResult);
                    return View("Login");
                }
            }

            //登入
            return await SignIn(viewModel, returnUrl);
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> EmailConfirmed(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "EmailConfirmed" : "Error");
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
            var user = UserManager.FindByEmail(viewModel.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ModelState.AddModelError("", "You must have a confirmed email to log on.");
                    return View("Login", viewModel);
                }
                //登入
                var signInResult = await SignInManager.PasswordSignInAsync(user.UserName, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
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
            else
            {
                ModelState.AddModelError("", "登入嘗試失試。");
                return View("Login", viewModel);
            }
        }

        private async Task SendEmailConfirmationAsync(TicketHubUser newUser)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
            var callbackUrl = Url.Action("EmailConfirmed", "Account", new { userId = newUser.Id, code = code }, protocol: Request.Url.Scheme);
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/MailTemplate/AccountConfirmation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ConfirmationLink}", callbackUrl);
            body = body.Replace("{UserName}", newUser.Email);
            await UserManager.SendEmailAsync(newUser.Id, "Confirm your TicketHub account", body);
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
