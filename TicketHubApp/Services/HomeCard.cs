using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class HomeCard
    {
        //Action

        //最新推出
        public SortNewCardListViewModel GetSortNewCard()
        {
            var result = new SortNewCardListViewModel();
            result.Items = new List<SortNewCardViewModel>();
            TicketHubContext context = new TicketHubContext();
            GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);



            var cardList = issueRepo.GetAll();

            foreach (var item in cardList)
            {
                var p = new SortNewCardViewModel()
                {
                    ImgPath = item.ImgPath,
                    Title = item.Title
                };

                result.Items.Add(p);

            }
            return result;
        }

        //熱賣票劵
        //public BestSellerCardViewModel GetBestSellerCard()
        //{
        //    var result = new BestSellerCardListViewModel();
        //    result.Items = new List<BestSellerCardViewModel>();
        //    TicketHubContext context = new TicketHubContext();
        //    GenericRepository<Issue> issueRepo = new GenericRepository<Issue>(context);



        //    var cardList = issueRepo.GetAll();

        //    foreach (var item in cardList)
        //    {
        //        var p = new SortNewCardViewModel()
        //        {
        //            ImgPath = item.ImgPath,
        //            Title = item.Title
        //        };

        //        result.Items.Add(p);

        //    }
        //    return result;
        //}
    }
}