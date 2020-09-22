using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopIssueViewModel
    {
        public Guid Id { get; set; }
        public Guid shopId { get; set; }
        [Required]
        [StringLength(128, MinimumLength = 3, ErrorMessage ="最少需要3個字元!")]
        public string Title { get; set; }
        public string Category { get; set; }
        public string Memo { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public string ImgPath { get; set; }
        [Required(ErrorMessage ="請輸入原價")]
        public decimal OriginalPrice { get; set; }
        [Required(ErrorMessage ="請輸入售價")]
        public decimal DiscountPrice { get; set; }
        public decimal DiscountRatio { get; set; }
        [Required(ErrorMessage ="請輸入商品數量")]
        public decimal Amount { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime IssuedDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReleasedDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ClosedDate { get; set; }
        public string IssuerId { get; set; }

        public string Status { get; set; }
        public decimal SalesPrice { get; set; }
    }
}