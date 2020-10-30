using Microsoft.Ajax.Utilities;
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
        private TicketHubContext _context = TicketHubContext.Create();
        public IEnumerable<ShopIssueViewModel> SearchIssue(string input)
        {
            string[] search = input?.Split(' ');
            var repo = new GenericRepository<Issue>(_context);
            var issueList = repo.GetAll();
            var result = from i in issueList
                         join s in _context.Shop on i.ShopId equals s.Id
                         select new ShopIssueViewModel
                         {
                             Id = i.Id,
                             Memo = i.Memo,
                             Title = i.Title,
                             DiscountPrice = i.DiscountPrice,
                             ImgPath = i.ImgPath,
                             OriginalPrice = i.OriginalPrice,
                             DiscountRatio = i.DiscountRatio,
                             Category = i.Category,
                             City = s.City,
                             District = s.District,
                             SalesAmount = (from t in _context.Ticket
                                            where t.IssueId == i.Id
                                           select t).ToList().Count,
                             TagList = (from tg in _context.IssueTag
                                        join t in _context.Tag on tg.TagId equals t.Id
                                        where tg.IssueId == i.Id
                                        select t.Name).ToList()
                         };
            if(search != null)
            {
                foreach (var i in search)
                {
                    result = result.Where(x => x.City.Contains(i) || x.District.Contains(i) || x.Title.Contains(i) || x.Category.Contains(i));
                }
                return result;
            }
            else
            {
                return null;
            }
            

        }

        public IEnumerable<ShopIssueViewModel> SearchByTag(decimal maxPrice, decimal minPrice)
        {
            var shops = new GenericRepository<Shop>(_context).GetAll();
            var issues = new GenericRepository<Issue>(_context).GetAll();
            var result = from i in issues
                         join s in shops on i.ShopId equals s.Id
                         where i.DiscountPrice >= minPrice && i.DiscountPrice <= maxPrice
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
                             District = s.District,
                             TagList = (from tg in _context.IssueTag
                                         join t in _context.Tag on tg.TagId equals t.Id
                                         where tg.IssueId == i.Id
                                         select t.Name).ToList()
                         };
            return result;
        }

        public IEnumerable<Guid> GetUserFsavotite(string userId)
        {
            return _context.UserWishIssue.Where(x => x.UserId == userId).Select(x => x.IssueId).ToList();
        }
    }


}