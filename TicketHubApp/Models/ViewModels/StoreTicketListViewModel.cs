using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models.ViewModels
{
    public class StoreTicketListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string ImgPath { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal DiscountRatio { get; set; }
    }
}