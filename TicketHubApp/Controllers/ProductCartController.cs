using EllipticCurve;
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
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetIssueData(List<ProductCartAjaxViewModel> model)
        {

            using (var _context = new TicketHubContext())
            {
                var list = new List<ProductCartViewModel>();
                foreach(var m in model)
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
                        Amount = count
                    };
                    list.Add(t);
                }

                //var temp = _context.Issue.Where(x => model.Any(y => y.id == x.Id)).ToList();

                string result = JsonConvert.SerializeObject(list);
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        //var local = local;
        //var temp = JsonConvert.DeserializeObject<List<object>>(cartList);
        //var context = new TicketHubContext();

        //var issues = from i in context.Issue
        //             where IssueId.Contains(i.Id.ToString())
        //             select i;


        //using (var _context = TicketHubContext.Create())
        //{
        //    foreach (var i in IssueId)
        //    {
        //        var temp = i;
        //        var issueGUID = Guid.Parse(i);
        //        //var StringTemp = string.Join(",", temp);
        //        Amount = (Amount == null) ? "1" : Amount;

        //        var temp_amount = decimal.Parse(Amount);
        //        var orderDetail = _context.Issue.Where(x => x.Id == issueGUID).Select(x => new OrderDetail
        //        {
        //            IssueId = x.Id,
        //            Amount = temp_amount,
        //            Price = x.DiscountPrice
        //        }).ToList();
        //        var order = new Order
        //        {
        //            OrderedDate = new DateTime(),
        //            OrderDetails = orderDetail
        //            //UserId = UserId
        //        };
        //        _context.Order.Add(order);
        //        _context.SaveChanges();

        //    }
        //}

        //return Content("");
    }

}