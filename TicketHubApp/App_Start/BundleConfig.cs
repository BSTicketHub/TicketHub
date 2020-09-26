using System.Web;
using System.Web.Optimization;

namespace TicketHubApp
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //----------------JS----------------
            bundles.Add(new ScriptBundle("~/bundles/js/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/js/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap").Include(
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/iconify").Include(
                      "~/Assets/JavaScript/Common/iconify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/chartjs").Include(
                      "~/Scripts/Chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/momentjs").Include(
                      "~/Scripts/moment.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/js/sidemenu").Include(
                      "~/Assets/JavaScript/Common/SideMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/carouseljs").Include(
                      "~/Assets/JavaScript/Common/owl.carousel.min.js"));

            //----------------CSS----------------

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/css/carousel").Include(
                      "~/Assets/CSS/Common/owl.carousel.min.css",
                      "~/Assets/CSS/Common/owl.theme.default.css"
                      ));
            bundles.Add(new StyleBundle("~/bundles/css/Site")
                .Include("~/Assets/CSS/Common/Site.min.css"));

            //----------------single bundle----------------single bundle----------------

            //Login page bundles
            bundles.Add(new StyleBundle("~/bundles/css/Login")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/Common/Site.min.css")
                .Include("~/Assets/CSS/login.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/Login").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js",
                "~/Assets/JavaScript/Common/JsBarcode.all.min.js"));

            //Platform bundles
            bundles.Add(new StyleBundle("~/bundles/css/Platform").Include(
                    "~/Content/bootstrap.css",
                    "~/Assets/CSS/Platform/Platform_Layout.min.css",
                    "~/Assets/CSS/Platform/DashBoard/Member.min.css",
                    "~/Assets/CSS/Platform/datatables.min.css",
                    "~/Assets/CSS/Platform/jquery.dataTables.css"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/js/Platform").Include(
                      "~/Scripts/Chart.js",
                      "~/Scripts/modernizr-*",
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/Assets/JavaScript/Common/iconify.min.js",
                      "~/Assets/JavaScript/Platform/datatables.min.js"
                ));

            //Home page
            bundles.Add(new StyleBundle("~/bundles/css/Home").Include(
                "~/Assets/CSS/Common/owl.carousel.min.css",
                "~/Assets/CSS/Common/owl.theme.default.css",
                "~/Assets/CSS/home.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/js/Home").Include(
                "~/Assets/JavaScript/Common/owl.carousel.min.js",
                "~/Scripts/esm/popper.min.js",
                "~/Assets/JavaScript/Home/home.js"));

            //CustomerPage bundles
            bundles.Add(new StyleBundle("~/bundles/css/CustomerPage")
                .Include("~/Assets/CSS/CustomerPage.min.css"));
            //bundles.Add(new ScriptBundle("~/bundles/js/CustomerPage").Include(
            //    "~/Scripts/bootstrap.js",
            //    "~/Assets/JavaScript/Common/iconify.min.js",
            //    "~/Scripts/moment.js"
            //    ));

            //ShopList bundles
            bundles.Add(new StyleBundle("~/bundles/css/ShopList")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/ShopList.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js/ShopList").Include(
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js"
                ));

            //ProductCart page bundles
            bundles.Add(new StyleBundle("~/bundles/css/Cart")
                .Include("~/Assets/CSS/ProductCart.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/Cart").Include(
                "~/Assets/JavaScript/ProductCart/ProductCart.js"
                ));

            //ProductDescription bundles
            bundles.Add(new StyleBundle("~/bundles/css/ProductDescription")
                .Include("~/Assets/CSS/ProductDescription.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/js/ProductDescription").Include(
                "~/Assets/JavaScript/Product/ProductDescription.js"
                ));

            //Shop bundles
            bundles.Add(new StyleBundle("~/bundles/css/Shop")
                .Include("~/Content/bootstrap.css")
                .Include("~/Assets/CSS/Shop/StoreBasic.css")
                .Include("~/Assets/CSS/Shop/ShopLayout.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js/Shop").Include(
                "~/Scripts/umd/popper.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/Chart.js",
                "~/Scripts/bootstrap.js",
                "~/Assets/JavaScript/Common/iconify.min.js",
                "~/Scripts/moment.js"));
        }
    }
}
