using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class SortNewCardViewModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Local { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Sold { get; set; }
        public decimal Amount { get; set; }
        public string[] Tag { get; set; } 
    }
}