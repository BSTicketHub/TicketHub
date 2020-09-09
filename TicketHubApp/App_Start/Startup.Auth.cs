using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using TicketHubDataLibrary.Models;

namespace TicketHubApp
{
    public partial class Startup
    {
        // 如需設定驗證的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 設定資料庫內容、使用者管理員和登入管理員，以針對每個要求使用單一執行個體
            app.CreatePerOwinContext(TicketHubContext.Create);
            app.CreatePerOwinContext<AppIdentityUserManager>(AppIdentityUserManager.Create);
            app.CreatePerOwinContext<AppIdentitySignInManager>(AppIdentitySignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppIdentityUserManager, TicketHubUser>(validateInterval: TimeSpan.FromMinutes(30), regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
