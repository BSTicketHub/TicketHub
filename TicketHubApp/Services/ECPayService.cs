using ECPayLibrary.Model;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        public AioBaseModel CreatECPayModel(string Id, string Amount)
        {
            AioBaseModel model = new AioBaseModel()
            {
                MerchantTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss"),
                MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                TradeDesc = "TicketHub!",
                ChoosePayment = "Credit"
            };

            var idArr = Id.Split('#');
            var amountArr = Amount.Split('#');
            var itemName = new List<string>();
            int total = 0;
            using(var _context = TicketHubContext.Create())
            {
                for(var i = 0; i < idArr.Length; i++)
                {
                    var id = Guid.Parse(idArr[i]);
                    itemName.Add(_context.Issue.Find(id).Title + " x " + amountArr[i] + "張");
                    var price = _context.Issue.Find(id).DiscountPrice * int.Parse(amountArr[i]);
                    total += (int)price;
                }
            }

            model.ItemName = string.Join("#", itemName);
            model.TotalAmount = total;

            return model;
        }

        public SortedDictionary<string, string> CreatePost(AioBaseModel model)
        {

            //### 建立Service
            var service = new ECPayService();

            //### 組合檢查碼
            string MerchantID = "2000132";
            string HashKey = "5294y06JbISpM5x9";
            string HashIV = "v77hoKGq4kWxNNIS";

            SortedDictionary<string, string> PostCollection = new SortedDictionary<string, string>();
            PostCollection.Add("MerchantID", MerchantID);
            PostCollection.Add("MerchantTradeNo", model.MerchantTradeNo);
            PostCollection.Add("MerchantTradeDate", model.MerchantTradeDate);
            PostCollection.Add("PaymentType", "aio");//固定帶aio
            PostCollection.Add("TotalAmount", model.TotalAmount.ToString());
            PostCollection.Add("TradeDesc", model.TradeDesc);
            PostCollection.Add("ItemName", model.ItemName);
            PostCollection.Add("ReturnURL", "http://tickethub-dev.azurewebsites.net//ProductCart/GetResultFromECPAY");//廠商通知付款結果API
            PostCollection.Add("ClientBackURL", "http://tickethub-dev.azurewebsites.net/");
            PostCollection.Add("ChoosePayment", model.ChoosePayment);
            PostCollection.Add("EncryptType", "1");//固定

            //壓碼
            string str = string.Empty;
            string str_pre = string.Empty;
            foreach (var item in PostCollection)
            {
                str += string.Format("&{0}={1}", item.Key, item.Value);
            }

            str_pre += string.Format("HashKey={0}" + str + "&HashIV={1}", HashKey, HashIV);

            string urlEncodeStrPost = HttpUtility.UrlEncode(str_pre);
            string ToLower = urlEncodeStrPost.ToLower();
            string sCheckMacValue = service.GetSHA256(ToLower);
            PostCollection.Add("CheckMacValue", sCheckMacValue);

            return PostCollection;
        }
    }
}
