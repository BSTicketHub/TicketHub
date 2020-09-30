using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class SimpleIssueViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ImgPath { get; set; }
    }
}