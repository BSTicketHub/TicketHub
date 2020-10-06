using EllipticCurve;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;
using TicketHubApp.Service;

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
            if (ModelState.IsValid) {

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

                    for (var j = 0; j < model.id.Count; j++)
                    {
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

                    //_context.SaveChanges();

                    return Content("123");
                }
            }
            else
            {
                return Content("Invalid input");
            }
        }

        [HttpPost]
        public ActionResult PostToECPay(string Id, string Amount)
        {
            if (ModelState.IsValid)
            {
                var Payservice = new ECPayService();
                var payModel = Payservice.CreatECPayModel(Id, Amount);
                var PostCollection = Payservice.CreatePost(payModel);
                string PostURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut";

                string ParameterString = string.Join("&", PostCollection.Select(p => p.Key + "=" + p.Value));

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<html><body>").AppendLine();
                sb.Append("<form name='ECPayAIO'  id='ECPayAIO' action='" + PostURL + "' method='POST'>").AppendLine();
                foreach (var aa in PostCollection)
                {
                    sb.Append("<input type='hidden' name='" + aa.Key + "' value='" + aa.Value + "'>").AppendLine();
                }

                sb.Append("</form>").AppendLine();
                sb.Append("<script> var theForm = document.forms['ECPayAIO'];  if (!theForm) { theForm = document.ECPayAIO; } theForm.submit(); </script>").AppendLine();
                sb.Append("<html><body>").AppendLine();

                TempData["PostForm"] = sb.ToString();
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public string GetResultFromECPAY(FormCollection formData)
        {
            string merchantId = formData.Get("MerchantID");
            string rtnCode = formData.Get("RtnCode");
            string check = formData.Get("CheckMacValue");
            string HashKey = "5294y06JbISpM5x9";
            string HashIV = "v77hoKGq4kWxNNIS";

            var service = new ECPayService();

            string str = string.Empty;
            string str_pre = string.Empty;
            Dictionary<string, string> response = new Dictionary<string, string>();
            for(var i = 0; i < formData.Count - 1; i++)
            {
                response.Add(formData.GetKey(i), formData.Get(formData.GetKey(i)));
            };
            response = response.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            foreach(var i in response)
            {
                str += string.Format("&{0}={1}", i.Key, i.Value);      
            }

            str_pre += string.Format("HashKey={0}" + str + "&HashIV={1}", HashKey, HashIV);

            string urlEncodeStrPost = HttpUtility.UrlEncode(str_pre);
            string ToLower = urlEncodeStrPost.ToLower();
            string sCheckMacValue = service.GetSHA256(ToLower);
            string MacValue = sCheckMacValue.ToUpper();

            if(merchantId == "2000132" && rtnCode == "1" && check == MacValue)
            {
                return "1|OK";
            } else
            {
                return "0|ErrorMessage";
            }
        }
    }
}

