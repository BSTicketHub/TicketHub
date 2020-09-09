using System.Web;
using System.Web.Optimization;

namespace TicketHubApp
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //Login page bundles
            bundles.Add(new StyleBundle("~/bundles/css/Login")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/login.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/Login").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/Common/JsBarcode.all.min.js"
                ));

            //CustomerPage bundles
            bundles.Add(new StyleBundle("~/bundles/css/CustomerDetail")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/CustomerDetail.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js/CustomerDetail").Include(
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/CustomerDetail/CustomerDetail.js"
                ));
            //ShopList bundles
            bundles.Add(new StyleBundle("~/bundles/css/ShopList")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/ShopList.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js/ShopList").Include(
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/ShopList/ShopList.js"
                ));

            //Cart page bundles
            bundles.Add(new StyleBundle("~/bundles/css/Cart")
                .Include("~/Assets/CSS/Cart.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/Cart").Include(
                "~/Assets/JavaScript/Cart/Cart.js"
                ));

            //payview page bundles
            bundles.Add(new StyleBundle("~/bundles/css/payview")
                .Include("~/Assets/CSS/payview.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/payview").Include(
                "~/Assets/JavaScript/Cart/payview.js"
                ));

            //ticket-description bundles
            bundles.Add(new StyleBundle("~/bundles/css/ticket-description")
                .Include("~/Assets/CSS/ticket-description.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/ticket-description").Include(
                "~/Assets/JavaScript/Product/ticket-description.js"
                ));
        }
    }
}
