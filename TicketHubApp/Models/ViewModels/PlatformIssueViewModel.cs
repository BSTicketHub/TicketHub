using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformIssueViewModel
    {   
        [Display(Name = "票券代號")]
        public Guid Id { get; set; }
        [Display(Name = "票券名稱")]
        public string Title { get; set; }
        [Display(Name = "原價")]
        public decimal OriginalPrice { get; set; }
        [Display(Name = "折扣後價格")]
        public decimal DiscountPrice { get; set; }
        [Display(Name = "折扣比率")]
        public decimal DiscountRatio { get; set; }
        [Display(Name = "總發行張數")]
        public decimal Amount { get; set; }
        [Display(Name = "已售出張數")]
        public decimal SelledAmount { get; set; }
        [Display(Name = "庫存張數")]
        public decimal Stock { get; set; }
        [Display(Name = "已兌換張數")]
        public decimal ExchangeAmount { get; set; }
        [Display(Name = "發行時間")]
        public DateTime IssuedDate { get; set; }
        [Display(Name = "釋出時間")]
        public DateTime ReleasedDate { get; set; }
        [Display(Name = "下架時間")]
        public DateTime ClosedDate { get; set; }
        [Display(Name = "發行人代號")]
        public string IssuerId { get; set; }
        [Display(Name = "發行人名稱")]
        public string UserName { get; set; }
        [Display(Name = "票券所屬商家代號")]
        public Guid ShopId { get; set; }
        [Display(Name = "票券所屬商家名稱")]
        public string ShopName { get; set; }
    }
}