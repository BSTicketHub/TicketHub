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
        public SortNewCardListViewModel GetSortNewCard()
        {
            var result = new SortNewCardListViewModel();
            result.Items = new List<SortNewCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);



            var cardList = issueRepo.GetAll();

            ////排序
            //string[] words = { "台式", "中式", "日式", "韓式", "美式", "泰式", "西式", "法式", "印度料理", "越南料理" };

            //IEnumerable<string> query = from word in words
            //                            orderby word.Length
            //                            select word;


            foreach (var item in cardList)
            {
                var p = new SortNewCardViewModel()
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

        //熱賣票劵
        public CarouselCardListViewModel GetBestSellerCard()
        {
            var result = new CarouselCardListViewModel();
            result.Items = new List<CarouselCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);


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