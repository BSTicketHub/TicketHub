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
        private DbContext _context;
        private IRepository<Order> _orderRepository;
        private IRepository<OrderDetail> _orderDetailRepository;

        private IRepository<Order> OrderRepository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        private IRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                if (_context == null)
                {
                    _context = new TicketHubContext();
                }
                if (_orderDetailRepository == null)
                {
                    _orderDetailRepository = new GenericRepository<OrderDetail>(_context);
                }
                return _orderDetailRepository;
            }
        }

        public PlatformOrderService()
        {
        }
        public PlatformOrderService(DbContext context)
        {
            _context = context;
        }

        //public List<PlatformOrderViewModel> GetAllOrders()
        //{   
        //    IQueryable<Order> orderList = OrderRepository.GetAll();
        //    IQueryable<OrderDetail> orderDetailList = OrderDetailRepository.GetAll();

        //    List<PlatformOrderViewModel> orders = new List<PlatformOrderViewModel>(orderList.Count());

        //    foreach (var item in orderList)
        //    {            
        //        PlatformOrderViewModel order = new PlatformOrderViewModel
        //        {
        //            Id = item.Id,
        //            OrderDate = item.OrderedDate,
        //            UserId = item.UserId,
        //            TotalPrice = orderDetailList.Where(x => x.OrderId == item.Id).Sum(x => x.Price)
        //        };
        //        orders.Add(order);
        //    }
        //    return orders;
        //}
    }
}