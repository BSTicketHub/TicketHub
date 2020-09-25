using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class HomeListViewModel
    {

        public BestSellerCardListViewModel BestSellerItems { get; set; }
        public RecommenCardListViewModel RecommenItems { get; set; }
        public SortNewCardListViewModel SortNewItems { get; set; }
        public LimitedtimeListViewModel LimitedtimeItems { get; set; }
    }
}