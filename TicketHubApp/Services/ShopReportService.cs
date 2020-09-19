using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopReportService
    {
        public List<string> getSalesReport(List<string> duration)
        {
            TicketHubContext context = new TicketHubContext();

            //var user = HttpContext.Current.User.Identity.GetUserId();
            var user = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4";
            var shopId = context.ShopEmployee.Where((x) => x.UserId == user).FirstOrDefault().ShopId;


            var temp = from od in context.OrderDetail
                       join i in context.Issue on od.IssueId equals i.Id
                       join o in context.Order on od.OrderId equals o.Id
                       where i.ShopId == shopId
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
            var id = Guid.Parse("FA10840D-3A73-4374-AAFD-D592A3623EC1");
            TicketHubContext context = new TicketHubContext();

            var temp = from i in context.Issue.Where(i => i.ShopId == id)
                       join od in context.OrderDetail on i.Id equals od.IssueId
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
            var id = Guid.Parse("FA10840D-3A73-4374-AAFD-D592A3623EC1");
            TicketHubContext context = new TicketHubContext();

            var temp = from i in context.Issue.Where(i => i.ShopId == id)
                       join od in context.OrderDetail on i.Id equals od.IssueId
                       join o in context.Order on od.OrderId equals o.Id
                       join u in context.Users on o.UserId equals u.Id
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
                var date = DateTime.Parse(item).AddMonths(1).ToString();
                var duration = new List<string> { date, date };
                var temp = getSalesReport(duration);
                var data = (Type == 0) ? temp[2] : temp[0];
                result[Label.IndexOf(item)] = Decimal.ToInt32(Convert.ToDecimal(data));
            }
            return result;
        }
    }
}
