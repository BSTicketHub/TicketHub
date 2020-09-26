using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
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
                    menuItems = new List<SideMenuItem>
                    {
                       new SideMenuItem{ IconName = "carbon:dashboard-reference", MenuTitle = "報表分析", Href = "/Platform/Index"},

                       new SideMenuItem{ IconName = "teenyicons:users-solid", MenuTitle = "會員管理", Href = "#UserManagement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "carbon:user-profile", MenuTitle = "會員列表", Href = "/Platform/UserList" },
                                new SideMenuItem { IconName = "bx:bxs-user-plus", MenuTitle = "新增會員", Href = "#" },
                            }
                       },

                       new SideMenuItem{ IconName = "si-glyph:store", MenuTitle = "商家管理", Href = "#ShopManagement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "la:store-solid", MenuTitle = "商家列表", Href = "/Platform/ShopList" },
                                new SideMenuItem { IconName = "ic:baseline-add-business", MenuTitle = "新增商家", Href = "#" },
                            }
                       },
                       new SideMenuItem{ IconName = "mdi:clipboard-list-outline", MenuTitle = "訂單管理", Href = "/Platform/OrderList"},
                       new SideMenuItem{ IconName = "clarity:administrator-solid", MenuTitle = "管理員管理", Href = "#AdminManagement",
                            SubMenuItems = new List<SideMenuItem>{
                                new SideMenuItem { IconName = "gg:user-list", MenuTitle = "管理員列表", Href = "/Platform/AdminList" },
                                new SideMenuItem { IconName = "subway:admin-1", MenuTitle = "新增管理員", Href = "#" },
                            }
                       },
                    };
                    break;
                default:
                    break;
            }

            var userLogo = new ImgurService().getSideMenuImage(page);
            ViewBag.Image = userLogo[0];
            ViewBag.Name = userLogo[1];
            return PartialView("_SideMenu", menuItems);
        }

        public class SideMenuItem
        {
            public string IconName { get; set; }
            public string MenuTitle { get; set; }
            public string Href { get; set; }
            public ICollection<SideMenuItem> SubMenuItems { get; set; }
        }

        public ActionResult GenLoginPartial()
        {
            var userLogo = new ImgurService().getSideMenuImage(PageType.CUSTOMER);
            string userImgPath = userLogo[0];

            ViewBag.userImg = userImgPath;
            return PartialView("_LoginPartial");
        }
    }
}
