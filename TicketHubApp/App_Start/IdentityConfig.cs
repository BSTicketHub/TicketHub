using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketHubDataLibrary.Models;

namespace TicketHubApp
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 將您的電子郵件服務外掛到這裡以傳送電子郵件。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 將您的 SMS 服務外掛到這裡以傳送簡訊。
            return Task.FromResult(0);
        }
    }

    public class AppIdentityUserManager : UserManager<TicketHubUser>
    {
        public AppIdentityUserManager(IUserStore<TicketHubUser> store) : base(store)
        {
        }

        public static AppIdentityUserManager Create(IdentityFactoryOptions<AppIdentityUserManager> options, IOwinContext context)
        {
            var manager = new AppIdentityUserManager(new UserStore<TicketHubUser>(context.Get<TicketHubContext>()));

            // 設定 usernames 驗證邏輯
            manager.UserValidator = new UserValidator<TicketHubUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 設定密碼驗證邏輯
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // 設定 user 預設鎖定
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 設定兩因子驗證 (two factor authentication). 這邊示範使用簡訊及 Emails
            manager.RegisterTwoFactorProvider("電話代碼", new PhoneNumberTokenProvider<TicketHubUser>
            {
                MessageFormat = "您的安全碼為 {0}"
            });
            manager.RegisterTwoFactorProvider("電子郵件代碼", new EmailTokenProvider<TicketHubUser>
            {
                Subject = "安全碼",
                BodyFormat = "您的安全碼為 {0}"
            });
            // 設定 Email 服務
            manager.EmailService = new EmailService();
            // 設定簡訊服務
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<TicketHubUser>(dataProtectionProvider.Create("TicketHub ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class AppIdentitySignInManager : SignInManager<TicketHubUser, string>
    {
        public AppIdentitySignInManager(AppIdentityUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(TicketHubUser user)
        {
            return user.GenerateUserIdentityAsync((AppIdentityUserManager)UserManager);
        }

        public static AppIdentitySignInManager Create(IdentityFactoryOptions<AppIdentitySignInManager> options, IOwinContext context)
        {
            return new AppIdentitySignInManager(context.GetUserManager<AppIdentityUserManager>(), context.Authentication);
        }
    }
}
