using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace ECPayLibrary.Service
{
    class ECPayService
    {
        public string GetSHA256(string ToLower)
        {
            SHA256 SHA256Hasher = SHA256.Create();
            byte[] data = SHA256Hasher.ComputeHash(Encoding.Default.GetBytes(ToLower));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }

        public AioBaseModel CreatECPayModel(ProductCartCrOrViewModel input)
        {
            AioBaseModel model = new AioBaseModel()
            {
                MerchantTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss"),
                MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                TradeDesc = "TicketHub!",
                ChoosePayment = "Credit"
            };

            var ItemName = new List<string>();
            decimal total = 0;
            using(var _context = TicketHubContext.Create())
            {
                for(var i = 0; i < input.id.Count; i++)
                {
                    ItemName.Add(_context.Issue.Find(input.id[i]).Title);
                    var price = _context.Issue.Find(input.id[i]).DiscountPrice * input.amount[i];
                    total += price;
                }
            }

            model.ItemName = string.Join("#", ItemName);
            model.TotalAmount = (int)total;

            return model;
        }
    }
}
