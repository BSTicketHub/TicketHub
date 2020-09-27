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
    public class HomeCardService
    {

        private TicketHubContext _context = new TicketHubContext();
        //private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();
        //Action

        //限時優惠
        public LimitedtimeListViewModel GetLimitedtimeCard()
        {
            var result = new LimitedtimeListViewModel();
            result.Items = new List<LimitedtimeViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);

            var cardList = issueRepo.GetAll();

            foreach (var item in cardList)
            {
                var p = new LimitedtimeViewModel()
                {
                    Id = item.Id,
                    Memo = item.Memo,
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    OriginalPrice = item.OriginalPrice,
                    DiscountPrice = item.DiscountPrice
                };

                result.Items.Add(p);

            }
            return result;
        }

        //最新推出
        public SortNewCardListViewModel GetSortNewCard(int count)
        {
            var numOfEachTimes = 6;
            var result = new SortNewCardListViewModel();
            result.Items = new List<SortNewCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);

            var temp = from i in context.Issue
                       join od in context.OrderDetail on i.Id equals od.IssueId into j
                       from od in j.DefaultIfEmpty()
                       select new
                       {
                           i.Id,
                           i.ImgPath,
                           i.Title,
                           i.Memo,
                           i.OriginalPrice,
                           i.DiscountPrice,
                           i.IssuedDate,
                           amount = od == null ? 0 : od.Amount
                       };
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.Memo,t.OriginalPrice, t.DiscountPrice, t.IssuedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.Memo,
                             g.Key.OriginalPrice,
                             g.Key.DiscountPrice,
                             g.Key.IssuedDate,
                             SalesAmount = g.Sum(x => x.amount),
                         };
            var cardList = issues.OrderByDescending(x => x.IssuedDate).Skip(count).Take(numOfEachTimes);

            


            foreach (var item in cardList)
            {
                var p = new SortNewCardViewModel()
                {
                    Id = item.Id,
                    Memo = item.Memo,
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    OriginalPrice = item.OriginalPrice,
                    DiscountPrice = item.DiscountPrice,
                    Amount = (int)item.SalesAmount
                };

                result.Items.Add(p);

            }
            return result;
        }

        //熱賣票劵
        public CarouselCardListViewModel GetBestSellerCard()
        {
            var result = new CarouselCardListViewModel();
            result.Items = new List<CarouselCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);

            //第一步驟
            //排序值
            var temp = from i in _context.Issue
                       join od in _context.OrderDetail on i.Id equals od.IssueId into j
                       from od in j.DefaultIfEmpty()
                       select new
                       {
                           i.Id,
                           i.ImgPath,
                           i.Title,
                           i.DiscountPrice,
                           i.ReleasedDate,
                           count = od == null ? 0 : od.Amount,
                       };
            // 排序需求
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.DiscountPrice, t.ReleasedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.DiscountPrice,
                             g.Key.ReleasedDate,
                             SalesCount = g.Sum(x => x.count),
                         };
            issues = issues.OrderByDescending(i => i.SalesCount); //降序

            var cardList = issueRepo.GetAll();
            var cardType = "bestseller";

            foreach (var item in cardList)
            {
                var p = new CarouselCardViewModel()
                {
                    CardType = cardType,
                    Id = item.Id,
                    Memo = item.Memo,
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    OriginalPrice = item.OriginalPrice,
                    DiscountPrice = item.DiscountPrice
                };

                result.Items.Add(p);

            }
            return result;
        }

        //推薦餐廳
        public CarouselCardListViewModel GetRecommenCard()
        {
            var result = new CarouselCardListViewModel();
            result.Items = new List<CarouselCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);


            var cardList = issueRepo.GetAll();
            var cardType = "recommend";

            foreach (var item in cardList)
            {
                var p = new CarouselCardViewModel()
                {
                    CardType = cardType,
                    Id = item.Id,
                    Memo = item.Memo,
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    OriginalPrice = item.OriginalPrice,
                    DiscountPrice = item.DiscountPrice
                };

                result.Items.Add(p);

            }
            return result;
        }


        //public HomeListViewModel GetHomeList()
        //{

        //    var result = new List<HomeListViewModel>();
        //    TicketHubContext context = new TicketHubContext();
        //    GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);


        //    var cardList = issueRepo.GetAll();

        //    foreach (var item in cardList)
        //    {
        //        var p = new HomeViewModel()
        //        {
        //            ImgPath = item.ImgPath,
        //            Title = item.Title,
        //            OriginalPrice = item.OriginalPrice,
        //            DiscountPrice = item.DiscountPrice
        //        };

        //        result.Add(p);

        //    }
        //    return result;
        //}

    }
}