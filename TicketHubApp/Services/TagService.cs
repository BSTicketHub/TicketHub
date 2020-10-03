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
        private string[] _categories = { "台式", "中式", "日式", "韓式", "美式", "泰式", "西式", "法式", "印度料理", "越南料理", "燒肉", "火鍋", "熱炒" };
        public List<SelectListItem> GenCategory()
        {
            return GenSelectList(_categories);
        }

        public List<SelectListItem> GenSelectList(IEnumerable<string> list)
        {
            var result = new List<SelectListItem>();
            foreach (var item in list)
            {
                result.Add(new SelectListItem() { Text = $"{item}", Value = $"{item}" });
            }
            return result;
        }
    }
}