using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketHubApp.Interfaces;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class PlatformOrderService
    {
        public DataTableViewModel GetAllOrders()
        {
            var result = new List<PlatformOrderViewModel>();
            var context = new TicketHubContext();

            var orderRepository = new GenericRepository<Order>(context);
            var orderDetailRepository = new GenericRepository<OrderDetail>(context);

            var temp = from o in orderRepository.GetAll()
                       join od in orderDetailRepository.GetAll()
                       on o.Id equals od.OrderId
                       select new
                       {
                           Id = o.Id,
                           OrderDate = o.OrderedDate,
                           UserId = o.UserId,
                           UserName = o.User.UserName,
                           Price = od.Price,
                       };

            var group = from t in temp
                        group t by new { t.Id, t.OrderDate, t.UserId, t.UserName } into g
                        select new PlatformOrderViewModel
                        {
                            Id = g.Key.Id,
                            UserId = g.Key.UserId,
                            OrderDate = g.Key.OrderDate,
                            UserName = g.Key.UserName,
                            TotalPrice = g.Sum(x => x.Price)
                        };

            DataTableViewModel table = new DataTableViewModel();
            table.data = new List<List<string>>();

            foreach (var item in group)
            {
                List<string> dataInstance = new List<string>();

                dataInstance.Add(item.Id.ToString());
                dataInstance.Add(item.UserId);
                dataInstance.Add(item.UserName);
                dataInstance.Add(item.TotalPrice.ToString());

                table.data.Add(dataInstance);
            }

            return table;
        }
    }
}