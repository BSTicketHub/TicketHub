using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopListService
    {
        public IEnumerable<ShopViewModel> SearchShop(string input)
        {
            var SearchString = (input == null) ? "" : string.Join("^", input.Split(' '));
            var _context = new TicketHubContext();
            GenericRepository<Shop> repo = new GenericRepository<Shop>(_context);
            var shopList = repo.GetAll();
            var result = shopList.Where(x => SearchString.Contains(x.City.Remove(2))).Select(x => new ShopViewModel
                {
                    Id = x.Id,
                    ShopName = x.ShopName,
                    ShopIntro = x.ShopIntro,
                    City = x.City,
                    District = x.District,
                    Address = x.Address,
                    Phone = x.Phone,
                    Geometry = x.Lat + " " + x.Lng,
                    Issues = _context.Issue.Where(y => y.ShopId == x.Id).Select(y => new SimpleIssueViewModel
                    {
                        Id = y.Id,
                        DiscountPrice = y.DiscountPrice,
                        Memo = y.Memo,
                        OriginalPrice = y.OriginalPrice,
                        Title = y.Title
                    })
            });

            return result;
        }

        public IEnumerable<Guid> GetUserFavotiteShop(string userId)
        {
            var _context = TicketHubContext.Create();
            var repo = new GenericRepository<UserFavoriteShop>(_context);
            var favoriteList = repo.GetAll().Where(x => x.UserId == userId).Select(x => x.ShopId).ToList();
            return favoriteList;
        } 
    }
}