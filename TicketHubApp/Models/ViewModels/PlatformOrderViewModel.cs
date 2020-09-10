using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformOrderViewModel
    {   
        [Display(Name = "訂單編號")]
        public Guid Id { get; set; }
        [Display(Name = "訂單日期")]
        public DateTime OrderDate{ get; set; }
        [Display(Name = "消費者編號")]
        public string UserId { get; set; }
        [Display(Name = "消費者名稱")]
        public string UserName { get; set; }
        [Display(Name = "訂單總金額")]
        public decimal TotalPrice { get; set; }
    }
}