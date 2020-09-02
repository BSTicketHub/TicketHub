using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHubApp.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public int shopId { get; set; }
        [Required]
        [StringLength(128, MinimumLength = 3, ErrorMessage ="最少需要3個字元!")]
        public string Title { get; set; }
        public string Memo { get; set; }
        [Required(ErrorMessage ="請輸入原價")]
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        [Required(ErrorMessage ="請輸入售價")]
        public decimal DiscountRatio { get; set; }
        [Required(ErrorMessage ="請輸入商品數量")]
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public int IssuerId { get; set; }
    }
}