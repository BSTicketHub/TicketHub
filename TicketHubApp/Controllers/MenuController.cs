using System.Collections.Generic;
using System.Web.Mvc;
using TicketHubApp.Services;
using TicketHubDataLibrary;

namespace TicketHubApp.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult GenSideMenu(string page)
        {
            ViewBag.Page = page;
            List<SideMenuItem> menuItems = null;
            switch (page)
            {
                case PageType.CUSTOMER:
                    menuItems = new List<SideMenuItem> {
                        new SideMenuItem{ IconName = "octicon:gear-24", MenuTitle = "會員資訊", Href = "#"},
                        new SideMenuItem{ IconName = "carbon:ticket", MenuTitle = "我的票券", Href = "#"},
                        new SideMenuItem{ IconName = "gg:heart", MenuTitle = "收藏票券", Href = "#"},
                        new SideMenuItem{ IconName = "bi:shop", MenuTitle = "收藏餐廳", Href = "#"},
                    };
                    break;
                case PageType.SHOP:
                    menuItems = new List<SideMenuItem> {
                        new SideMenuItem{ IconName = "zmdi:store", MenuTitle = "商家資訊", Href = "/Shop/ShopInfo"},
                        new SideMenuItem{ IconName = "carbon:ticket", MenuTitle = "商品管理", Href = "#productManegement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "票券上架", Href = "/Shop/CreateIssue" },
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "票券列表", Href = "/Shop/IssueList" },
                            }
                        },
                        new SideMenuItem{ IconName = "clarity:employee-line", MenuTitle = "員工管理", Href = "#employeeManagement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "員工列表", Href = "/Shop/EmployeeList"},
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "員工新增", Href = "/Shop/EmployeeCreate"},
                            } 
                        },
                        new SideMenuItem{ IconName = "carbon:text-link-analysis", MenuTitle = "銷售分析", Href = "#sellingAnalysis",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "clarity:details-line", MenuTitle = "報表分析", Href = "/Shop/SalesReport" },
                            }
                        },
                    };
                    break;
                case PageType.PLATFORM:

                    break;
                default:
                    break;
            }

            string sideMenuImg = new ImgurService().getSideMenuImage(page);
            ViewBag.Image = sideMenuImg;
            return PartialView("_SideMenu", menuItems);
        }

        public class SideMenuItem
        {
            public string IconName { get; set; }
            public string MenuTitle { get; set; }
            public string Href { get; set; }
            public ICollection<SideMenuItem> SubMenuItems { get; set; }
        }
    }
}
