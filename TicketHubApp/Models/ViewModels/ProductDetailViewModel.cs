using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime ReleasedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string IssuerId { get; set; }

        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }

        public ICollection<IssueTag> IssueTags { get; set; }
    }
}