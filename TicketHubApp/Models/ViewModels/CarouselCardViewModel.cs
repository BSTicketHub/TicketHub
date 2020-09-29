using System;

namespace TicketHubApp.Models.ViewModels
{
    public class CarouselCardViewModel
    {
        public Guid Id { get; set; }
        public string CardType { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string ImgPath { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal DiscountRatio { get; set; }
        public decimal Amount { get; set; }
    }
}