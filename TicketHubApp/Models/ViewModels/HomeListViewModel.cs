namespace TicketHubApp.Models.ViewModels
{
    public class HomeListViewModel
    {

        public CarouselCardListViewModel BestSellerItems { get; set; }
        public CarouselCardListViewModel RecommenItems { get; set; }
        public SortNewCardListViewModel SortNewItems { get; set; }
        public LimitedtimeListViewModel LimitedtimeItems { get; set; }
    }
}