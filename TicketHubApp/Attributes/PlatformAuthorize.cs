using System.Web.Mvc;

namespace TicketHubApp.Attributes
{
    public class PlatformAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //判斷!! 當是未登入使用者時
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Account/LoginPlatform" + "?returnUrl=" +
                filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                return;
            }

        }
    }
}
