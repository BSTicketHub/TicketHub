using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketHubApp.Services
{
    public class TagService
    {
        private string[] _categories = { "台式", "中式", "日式", "韓式", "美式", "泰式", "西式", "法式", "印度料理", "越南料理" };
        public List<SelectListItem> GenCategory()
        {
            return GenSelectList(_categories);
        }

    //    private string[] _countries = { "臺北市", "新北市", "基隆市", "桃園市", "新竹市", "新竹縣", "苗栗縣",
    //"臺中市", "彰化縣", "南投縣", "雲林縣", "嘉義市", "嘉義縣", "臺南市", "高雄市", "屏東縣",
    //"宜蘭縣", "花蓮縣", "臺東縣", "澎湖縣", "金門縣", "連江縣" };
    //    public List<SelectListItem> GenCountry()
    //    {
    //        return GenSelectList(_countries);
    //    }


        public List<SelectListItem> GenSelectList(IEnumerable<string> list)
        {
            var result = new List<SelectListItem>();
            foreach (var item in list)
            {
                result.Add(new SelectListItem() { Text = $"{item}", Value = $"{item}" });
            }
            result.First().Selected = true;
            return result;
        }
    }
}