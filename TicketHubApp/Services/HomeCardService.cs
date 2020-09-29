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
                           i.Amount,
                           i.OriginalPrice,
                           i.DiscountPrice,
                           i.ReleasedDate,
                           i.ClosedDate,
                           amount = od == null ? 0 : od.Amount,
                           TagList = (from tg in context.IssueTag
                                      join t in context.Tag on tg.TagId equals t.Id
                                      where tg.IssueId == i.Id
                                      select t.Name).ToList()
                       };
            var issues = from t in temp
                         group t by new { t.Id, t.ImgPath, t.Title, t.Amount, t.Memo,t.OriginalPrice, t.DiscountPrice, t.ReleasedDate, t.ClosedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.ImgPath,
                             g.Key.Title,
                             g.Key.Memo,
                             g.Key.Amount,
                             g.Key.OriginalPrice,
                             g.Key.DiscountPrice,
                             g.Key.ReleasedDate,
                             g.Key.ClosedDate,
                             SalesAmount = g.Sum(x => x.amount),
                             TagList = (from tg in context.IssueTag
                                        join t in context.Tag on tg.TagId equals t.Id
                                        where tg.IssueId == g.Key.Id
                                        select t.Name).ToList()
                         };
            var now = DateTime.Now;
            //issues = issues.Where(x => (x.ReleasedDate <= now) && (x.ClosedDate >= now));
            issues = issues.Where(x => (x.Amount - x.SalesAmount) > 0);
            var cardList = issues.OrderByDescending(x => x.ReleasedDate).Skip(count).Take(numOfEachTimes);

           
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
                    Amount = (int)item.SalesAmount,
                    TagList = item.TagList
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