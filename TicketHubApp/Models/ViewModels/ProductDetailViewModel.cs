using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{

    public class ProductDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public string ImgPath { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ShopName { get; set; }
        public string ShopIntro { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int Zip { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string IssuerId { get; set; }
    }
}