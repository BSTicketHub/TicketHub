using System.Web.Mvc;

namespace TicketHubApp.Attributes
{
    public class PlatformAuthorize : AuthorizeAttribute
    {
        private readonly string loginPath = "~/Account/LoginPlatform";

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string requestUrl = filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl);
            filterContext.Result = new RedirectResult(loginPath + "?returnUrl=" + requestUrl);
        }
    }
}
