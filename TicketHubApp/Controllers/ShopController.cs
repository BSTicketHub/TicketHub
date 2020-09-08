using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TicketHubApp.Controllers
{
    public class ShopController : Controller
    {
        // GET: ShopList
        public ActionResult ShopList()
        {
            return View();
        }

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult CreateProduct()
        {
            var categoryList = new List<SelectListItem>()
            {
                new SelectListItem{Text = "台式", Value= "category-1"},
                new SelectListItem{Text = "日式", Value= "category-2"},
                new SelectListItem{Text = "韓式", Value= "category-3"},
                new SelectListItem{Text = "中式", Value= "category-4"},
                new SelectListItem{Text = "美式", Value= "category-5"},
                new SelectListItem{Text = "泰式", Value= "category-6"},
                new SelectListItem{Text = "西式", Value= "category-7"},
                new SelectListItem{Text = "法式", Value= "category-8"},
                new SelectListItem{Text = "印度料理", Value= "category-9"},
                new SelectListItem{Text = "越南料理", Value= "category-10"}
            };
            categoryList.Where(q => q.Value == "category-1").First().Selected = true;

            ViewBag.CategoryList = categoryList;

            return View();
        }

        public ActionResult StoreInformation()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderDetails()
        {
            return View();
        }

        public ActionResult TicketDetails()
        {
            return View();
        }

        public ActionResult SalesReport()
        {
            return View();
        }
    }
}