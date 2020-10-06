using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopReportService
    {
        private TicketHubContext _context = new TicketHubContext();

        private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();

        public List<string> getSalesReport(List<string> duration)
        {
            Guid shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;

            var temp = from od in _context.OrderDetail
                       join i in _context.Issue on od.IssueId equals i.Id
                       join o in _context.Order on od.OrderId equals o.Id
                       where i.ShopId == shopid
                       select new { TotalSales = od.Amount * od.Price, TotalAmount = od.Amount, o.UserId, o.OrderedDate };

            var st = (String.IsNullOrEmpty(duration[0])) ? DateTime.MinValue : DateTime.Parse(duration[0]);
            var en = (String.IsNullOrEmpty(duration[1])) ? DateTime.MaxValue : DateTime.Parse(duration[1]);

            temp = temp.Where(d => (d.OrderedDate >= st) && (d.OrderedDate <= en));

            decimal TotalSales = 0;
            decimal TotalAmount = 0;
            int TotalCustom = 0;
            if (temp.Any())
            {
                TotalSales = temp.Sum(s => s.TotalSales);
                TotalAmount = temp.Sum(a => a.TotalAmount);
                TotalCustom = temp.GroupBy(x => x.UserId).Count();
            }

            List<string> result = new List<string>();
            result.Add(TotalSales.ToString());
            result.Add(TotalAmount.ToString());
            result.Add(TotalCustom.ToString());

            return result;
        }

        public IQueryable<Object> getTopIssue()
        {
            Guid shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;

            var temp = from i in _context.Issue.Where(i => i.ShopId == shopid)
                       join od in _context.OrderDetail on i.Id equals od.IssueId
                       select new
                       {
                           Id = i.Id,
                           Title = i.Title,
                           SellingAmount = od.Amount,
                           Price = od.Price
                       };
            var group = (from t in temp
                        group t by new { t.Id, t.Title } into g
                        orderby g.Sum(x => x.Price) descending
                        select new
                        {
                            Name = g.Key.Title,
                            IssueAmount = g.Sum(x => x.SellingAmount),
                            TotalRevenue = g.Sum(x => x.Price),
                        }).Take(5);
            return group;
        }

        public IQueryable<Object> getTopCutsom()
        {
            Guid shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;

            var temp = from i in _context.Issue.Where(i => i.ShopId == shopid)
                       join od in _context.OrderDetail on i.Id equals od.IssueId
                       join o in _context.Order on od.OrderId equals o.Id
                       join u in _context.Users on o.UserId equals u.Id
                       select new
                       {
                           UserId = o.UserId,
                           UserName = u.UserName,
                           IssueAmount = od.Amount,
                           IssuePrice = od.Price
                       };
            var group = (from t in temp
                        group t by new { t.UserId, t.UserName } into g
                        orderby g.Sum(x => x.IssuePrice) descending
                        select new
                        {
                            Name = g.Key.UserName,
                            IssueAmount = g.Sum(x => x.IssueAmount),
                            TotalRevenue = g.Sum(x => x.IssuePrice)
                        }).Take(5);
            return group;
        }

        public int[] getChartCustomer(List<string> Label, int Type)
        {
            var result = new int[Label.Count];
            foreach(var item in Label)
            {
                var date = DateTime.Parse(item);
                DateTime startDate;
                DateTime endDate;
                if (Label.Count < 10)
                {
                    startDate = date.Date;
                    endDate = date.Date.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    startDate = date.Date;
                    endDate = date.Date.AddMonths(1).AddSeconds(-1);
                }

                var duration = new List<string> { startDate.ToString(), endDate.ToString() };
                var temp = getSalesReport(duration);
                var data = (Type == 0) ? temp[2] : temp[0];
                result[Label.IndexOf(item)] = Decimal.ToInt32(Convert.ToDecimal(data));
            }
            return result;
        }
    }
}
