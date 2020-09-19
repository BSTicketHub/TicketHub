using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class TicketListService
    {
        public IEnumerable<ShopIssueViewModel> SearchIssue(string input)
        {
            var _context = TicketHubContext.Create();
            var repo = new GenericRepository<Issue>(_context);
            var issueList = repo.GetAll();
            var result = from i in issueList
                         join s in _context.Shop on i.ShopId equals s.Id
                         where s.City.Contains(input) || s.District.Contains(input)
                         select new ShopIssueViewModel
                         {
                             Id = i.Id,
                             Memo = i.Memo,
                             Title = i.Title,
                             DiscountPrice = i.DiscountPrice,
                             ImgPath = i.ImgPath,
                             OriginalPrice = i.OriginalPrice,
                             DiscountRatio = i.DiscountRatio,
                             City = s.City,
                             District = s.District
                         };

            return result;
        }
    }
}