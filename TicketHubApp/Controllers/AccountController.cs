using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;
using TicketHubDataLibrary;
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
                    return await SignIn(viewModel, RoleGroup.CUSTOMER, returnUrl);
                }

                var newUser = new TicketHubUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email
                };

                var createResult = await UserManager.CreateAsync(newUser, viewModel.Password);
                if (createResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(newUser.Id, RoleGroup.CUSTOMER);
                    await SendEmailConfirmationAsync(newUser);
                    return new InfoViewService().RegisterSuccess();
                }
                else
                {
                    AddErrors(createResult);
                    return View("Login", viewModel);
                }
            }

            //登入
            return await SignIn(viewModel, RoleName.CUSTOMER, returnUrl);
        }

        [AllowAnonymous]
        public async Task<ActionResult> EmailConfirmed(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return result.Succeeded ? new InfoViewService().EmailConfirmed() : View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // 不顯示使用者不存在
                    return new InfoViewService().CheckMail("Forgot Password");
                }

                await SendResetPasswordAsync(user);
                return new InfoViewService().CheckMail("Forgot Password");
            }

            return View("Login", model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // 不顯示使用者不存在
                return new InfoViewService().ResetPasswordConfirmation();
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return new InfoViewService().ResetPasswordConfirmation();
            }
            AddErrors(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShopLogin(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            return await SignIn(viewModel, RoleGroup.SHOP, returnUrl);
        }

        [HttpGet]
        public ActionResult LoginPlatform()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginPlatform(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            return await SignIn(viewModel, RoleGroup.PLATFORM, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShopApply()
        {
            var service = new ShopService();
            var employees = service.GetEmployees("ComeToEat");
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ShopApply(ShopApplyViewModel viewModel)
        {
            var context = new TicketHubContext();

            var service = new ShopService(context);
            using (var transaction = context.Database.BeginTransaction())
            {
                var createShopResult = service.CreateShop(viewModel);
                var userId = User.Identity.GetUserId();
                var addRoleResult = service.AddEmployeeWithRole(userId, viewModel.ShopName, RoleName.SHOP_MANAGER);

                if (createShopResult.Success && addRoleResult.Success)
                {
                    transaction.Commit();
                    return new InfoViewService().CommonSuccess("Shop Apply", "Shop already applied, please wait for administrator confirmed.");
                }

                transaction.Rollback();
            }
            return View(viewModel);
        }

        private async Task<ActionResult> SignIn(LoginViewModel viewModel, string roleGroup, string returnUrl)
        {
            var user = UserManager.FindByEmail(viewModel.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ModelState.AddModelError("", "You must have a confirmed email to log on.");
                    return View("Login", viewModel);
                }

                bool isUserInRole;
                switch (roleGroup)
                {
                    case RoleGroup.CUSTOMER:
                        isUserInRole = await UserManager.IsInRoleAsync(user.Id, RoleName.CUSTOMER);
                        break;
                    case RoleGroup.SHOP:
                        isUserInRole = (await UserManager.IsInRoleAsync(user.Id, RoleName.SHOP_EMPLOYEE) || await UserManager.IsInRoleAsync(user.Id, RoleName.SHOP_MANAGER));
                        break;
                    case RoleGroup.PLATFORM:
                        isUserInRole = (await UserManager.IsInRoleAsync(user.Id, RoleName.PLATFORM_ADMIN) || await UserManager.IsInRoleAsync(user.Id, RoleName.ADMINISTRATOR));
                        break;
                    default:
                        isUserInRole = false;
                        break;
                }
                if (!isUserInRole)
                {
                    ModelState.AddModelError("", "Check your account or password.");
                    return View("Login", viewModel);
                }

                //登入
                var signInResult = await SignInManager.PasswordSignInAsync(user.UserName, viewModel.Password, viewModel.RememberMe, shouldLockout: false);
                switch (signInResult)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(roleGroup, returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = viewModel.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Check your account or password.");
                        return View("Login", viewModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "Check your account or password.");
                return View("Login", viewModel);
            }
        }

        private async Task SendEmailConfirmationAsync(TicketHubUser newUser)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
            var callbackUrl = Url.Action("EmailConfirmed", "Account", new { userId = newUser.Id, code = code }, protocol: Request.Url.Scheme);
            string title = "Account Confirmation";
            string content = "Please click bellow button for confirm your account.";
            string linkText = "Confirmation Link";

            string body = SetMailTemplate(title, content, linkText, callbackUrl, newUser.Email);
            await UserManager.SendEmailAsync(newUser.Id, "Confirm your TicketHub account", body);
        }

        private async Task SendResetPasswordAsync(TicketHubUser user)
        {
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            string title = "Password Reset";
            string content = "Please click bellow button for reset your password.";
            string linkText = "Reset Link";

            string body = SetMailTemplate(title, content, linkText, callbackUrl, user.Email);
            await UserManager.SendEmailAsync(user.Id, "Reset your TicketHub password", body);
        }

        private string SetMailTemplate(string title, string content, string linkText, string callbackUrl, string userName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/MailTemplate/Confirmation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Title}", title);
            body = body.Replace("{Content}", content);
            body = body.Replace("{ConfirmationLink}", callbackUrl);
            body = body.Replace("{ConfirmationLinkText}", linkText);
            body = body.Replace("{UserName}", userName);
            return body;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string roleGroup, string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            switch (roleGroup)
            {
                case RoleGroup.CUSTOMER:
                    return RedirectToAction("Index", "Home");
                case RoleGroup.SHOP:
                    return RedirectToAction("Index", "Shop");
                case RoleGroup.PLATFORM:
                    return RedirectToAction("Index", "Platform");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
    }

}
