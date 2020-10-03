using EllipticCurve;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class ProductCartController : Controller
    {
        // GET: ProductCart
        public ActionResult ProductCart()
        {
            //進畫面的第一個action, 再此抓UserId

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                ViewBag.UserID = currentUserId;

            }
            return View();

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetIssueData(List<ProductCartAjaxViewModel> model)
        {

            using (var _context = new TicketHubContext())
            {
                var list = new List<ProductCartViewModel>();
                foreach (var m in model)
                {
                    var count = Decimal.Parse(m.amount);
                    //型別要與ProductCartAjaxViewModel相同才能比較
                    var item = _context.Issue.FirstOrDefault(x => x.Id == m.id);
                    //比對id抓到第一個一樣的就取該值
                    var t = new ProductCartViewModel()
                    {
                        Id = item.Id,
                        ImgPath = item.ImgPath,
                        Title = item.Title,
                        Memo = item.Memo,
                        OriginalPrice = item.OriginalPrice,
                        DiscountPrice = item.DiscountPrice,
                        Amount = count,
                        OrderDetailAmount = (from od in _context.OrderDetail
                                             where item.Id == od.IssueId
                                             select od.Amount).DefaultIfEmpty().Sum(),
                        IssuesAmount = item.Amount
                    };
                    list.Add(t);
                }

                //var temp = _context.Issue.Where(x => model.Any(y => y.id == x.Id)).ToList();

                string result = JsonConvert.SerializeObject(list);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult CreateOrder(ProductCartCrOrViewModel model)
        {
            //對應JS 接資料
            using (var _context = new TicketHubContext())
            {
                Order order = new Order()
                {
                    OrderedDate = DateTime.Now,
                    UserId = model.UserId,
                    OrderDetails = new List<OrderDetail>()
                };

                for (var i = 0; i < model.id.Count; i++)
                {
                    var price = _context.Issue.Find(model.id[i]).DiscountPrice;
                    OrderDetail od = new OrderDetail()
                    {
                        Amount = model.amount[i],
                        IssueId = model.id[i],
                        Price = price
                    };

                    order.OrderDetails.Add(od);
                }

                for (var j = 0; j< model.id.Count; j++){
                    for (var i = 0; i < model.amount[j]; i++)
                    {
                        Ticket ticket = new Ticket()
                        {
                            DeliveredDate = DateTime.Now,
                            Exchanged = false,
                            ExchangedDate = null,
                            Voided = false,
                            VoidedDate = null,
                            UserId = model.UserId,
                            IssueId = model.id[j],
                            OrderId = order.Id
                        };
                        _context.Ticket.Add(ticket);
                    }
                }
                    _context.Order.Add(order);

                    _context.SaveChanges();

                return Content("123");

            }

        }


    }

}