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

            bundles.Add(new ScriptBundle("~/bundles/js/Login").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/Common/JsBarcode.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                      "~/Scripts/Chart.js"
                ));

            //Home page
            bundles.Add(new StyleBundle("~/bundles/css/Home")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/tempCSS/owl.carousel.min.css")
                .Include("~/Assets/CSS/tempCSS/owl.theme.default.css")
                .Include("~/Assets/CSS/tempCSS/temphome.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/js/Home").Include(
                //"~/Scripts/jquery-3.5.1.min.js",
                //"~/Scripts/bootstrap.js",
                //"~/Assets/JavaScript/Home/popper.min.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/Home/owl.carousel.min.js",
                "~/Assets/JavaScript/Home/home.js"));

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
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/Chart.js"));
        }
    }
}
