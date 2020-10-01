using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class ProductCartViewModel
    {
        public Guid Id { get; set; }
        public DateTime OrderedDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Memo { get; set; }
        public string ImgPath { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }

        public decimal? OrderDetailAmount { get; set; }
        public decimal IssuesAmount { get; set; }


    }
}