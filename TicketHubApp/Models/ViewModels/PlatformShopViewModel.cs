using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformShopViewModel
    {
        [Display(Name = "商家代號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "商家名稱")]
        public string ShopName { get; set; }
        [Display(Name = "電話")]
        public string Phone { get; set; }
        [Display(Name = "傳真")]
        public string Fax { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "信箱")]
        public string Email { get; set; }
        [Display(Name = "網站")]
        public string Website { get; set; }
        public string BannerImg { get; set; }
        [Display(Name ="申請時間")]
        [DataType(DataType.DateTime)]
        public DateTime AppliedDate { get; set; }
        [Display(Name = "修改時間")]
        public DateTime ModifiedDate { get; set; }       
        public DateTime ValidatedDate { get; set; }
        public Guid? ReviewerId { get; set; }
    }
}