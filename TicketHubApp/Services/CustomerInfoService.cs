using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = _context.Users.Find(userId);
            var info = new CustomerInfoViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Sex = user.Sex,
                FavoriteShop = from s in _context.Shop
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
                WishIssue = from i in _context.Issue
                            join uwi in _context.UserWishIssue on i.Id equals uwi.IssueId
                            where uwi.UserId == user.Id
                            select new ShopIssueViewModel
                            {
                                DiscountPrice = i.DiscountPrice,
                                Id = i.Id,
                                Memo = i.Memo,
                                Title = i.Title,
                                OriginalPrice = i.OriginalPrice,
                            }
            };

            return info;
        }
    }
}