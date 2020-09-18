using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopReportService
    {
        public List<string> getSalesRepoet(List<string> duration)
        {
            TicketHubContext context = new TicketHubContext();

            //var user = HttpContext.Current.User.Identity.GetUserId();
            var user = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4";
            var shopId = context.ShopEmployee.Where((x) => x.UserId == user).FirstOrDefault().ShopId;


            var temp = from od in context.OrderDetail
                       join i in context.Issue on od.IssueId equals i.Id
                       join o in context.Order on od.OrderId equals o.Id
                       where i.ShopId == shopId
                       select new { TotalSales =  od.Amount * od.Price, TotalAmount = od.Amount, o.UserId, o.OrderedDate };

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

    }
}
