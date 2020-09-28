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
        public CarouselCardListViewModel GetBestSellerCard(bool closed)
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
                           i.OriginalPrice,
                           i.DiscountRatio,
                           i.ReleasedDate,
                           i.ClosedDate,
                           count = od == null ? 0 : od.Amount,
                       };
            // 排序需求
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.DiscountPrice, t.OriginalPrice, t.ReleasedDate, t.ClosedDate, t.DiscountRatio } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.DiscountPrice,
                             g.Key.OriginalPrice,
                             g.Key.DiscountRatio,
                             g.Key.ReleasedDate,
                             g.Key.ClosedDate,
                             SalesCount = g.Sum(x => x.count),
                         };
            issues = issues.OrderByDescending(i => i.SalesCount); //降序

            var TimeNow = DateTime.Now;
            // 判斷
            if (closed)
            {
                issues = issues.Where((i) => i.ClosedDate <= TimeNow);
            }
            else
            {
                issues = issues.Where((i) => (i.ClosedDate > TimeNow || i.ClosedDate == null));
            }

            //var cardList = issueRepo.GetAll();
            var cardType = "bestseller";

            foreach (var item in issues)
            {
                //頁面顯示資料
                var p = new CarouselCardViewModel()
                {
                    CardType = cardType,
                    Id = item.Id,
                    ImgPath = item.ImgPath,
                    Title = item.Title,
                    OriginalPrice = item.OriginalPrice,
                    DiscountPrice = item.DiscountPrice,
                    DiscountRatio = item.DiscountRatio
                };

                result.Items.Add(p);

            }
            return result;
        }

        //推薦餐廳
        public CarouselCardListViewModel GetRecommenCard(bool sort)
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
                           i.OriginalPrice,
                           i.ReleasedDate,
                           i.ClosedDate,
                           count = od == null ? 0 : od.Amount,
                       };
            // 排序需求
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.DiscountPrice, t.OriginalPrice, t.ReleasedDate, t.ClosedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.DiscountPrice,
                             g.Key.OriginalPrice,
                             g.Key.ReleasedDate,
                             g.Key.ClosedDate,
                             SalesCount = g.Sum(x => x.count),
                         };

            issues = issues.OrderByDescending(i => i.SalesCount); //降序

            var Price = DateTime.Now;
            //Array.Sort(nums);

            // 判斷
            if (sort)
            {
                issues = issues.Where((i) => i.ReleasedDate <= Price);
            }
            else
            {
                issues = issues.Where((i) => (i.ReleasedDate > Price || i.ReleasedDate == null));
            }


            //var cardList = issueRepo.GetAll();
            var cardType = "recommend";

            //找對對應值
            foreach (var item in issues)
            {
                var p = new CarouselCardViewModel()
                {
                    CardType = cardType,
                    Id = item.Id,
                    //Memo = item.Memo,
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