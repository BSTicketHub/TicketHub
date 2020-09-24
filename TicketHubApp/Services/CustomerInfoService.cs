using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class CustomerInfoService
    {
        public CustomerInfoViewModel GetInfo(string userId)
        {
            var _context = TicketHubContext.Create();
            var shop = new GenericRepository<Shop>(_context).GetAll();
            var issue = new GenericRepository<Issue>(_context).GetAll();
            var order = new GenericRepository<Order>(_context).GetAll();
            var ticket = new GenericRepository<Ticket>(_context).GetAll();
            var user = _context.Users.Find(userId);
            var info = new CustomerInfoViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Sex = user.Sex,
                FavoriteShop = from s in shop
                               join ufs in _context.UserFavoriteShop on s.Id equals ufs.ShopId
                               where ufs.UserId == user.Id
                               select new ShopViewModel
                               {
                                   Id = s.Id,
                                   ShopName = s.ShopName,
                                   ShopIntro = s.ShopIntro,
                                   City = s.City,
                                   District = s.District,
                                   Address = s.Address,
                                   Phone = s.Phone
                               },
                WishIssue = from i in issue
                            join uwi in _context.UserWishIssue on i.Id equals uwi.IssueId
                            where uwi.UserId == user.Id
                            select new ShopIssueViewModel
                            {
                                DiscountPrice = i.DiscountPrice,
                                Id = i.Id,
                                Memo = i.Memo,
                                Title = i.Title,
                                OriginalPrice = i.OriginalPrice,
                            },
                MyOrder = from od in _context.OrderDetail
                          join o in order on od.OrderId equals o.Id
                          where o.Id == od.OrderId
                          select new CustomerOrderViewModel
                          {
                              Id = o.Id,
                              OrderDate = o.OrderedDate.ToString(),
                              UserId = userId,
                              OrderIssue = from i in issue
                                           where i.Id == od.IssueId
                                           select new ShopIssueViewModel
                                           {
                                               DiscountPrice = i.DiscountPrice,
                                               Amount = od.Amount,
                                               Id = i.Id,
                                               Title = i.Title
                                           }
                          },
                MyTicket = from i in issue
                           join t in ticket on i.Id equals t.IssueId
                           where t.Exchanged == false && t.Voided == false && t.UserId == userId
                           select new TicketViewModel
                           {
                               IssueTitle = i.Title,
                               VoidedDate = i.ClosedDate.ToString(),
                               ImgPath = i.ImgPath,
                               TicketDetail = from o in order
                                              where o.Id == t.OrderId
                                              select new TicketDetailViewModel
                                              {
                                                  IssueId = i.Id,
                                                  ImgPath = i.ImgPath,
                                                  IssueTitle = i.Title,
                                                  OrderDate = o.OrderedDate.ToString(),
                                                  OrderId = o.Id,
                                                  OrderMaker = user.UserName,
                                                  CloseDate = i.ClosedDate.ToString(),
                                                  TicketId = from t in ticket
                                                             where t.UserId == userId && t.IssueId == i.Id
                                                             select t.Id
                                              }
                           },
                InvalidTicket = from t in ticket
                                join i in issue on t.IssueId equals i.Id
                                where t.Exchanged == true && t.UserId == userId
                                select new TicketViewModel
                                {
                                    ImgPath = i.ImgPath,
                                    IssueTitle = i.Title,
                                    ExchangedDate = t.ExchangedDate.ToString()
                                }
                           
            };

            return info;
        }
    }
}