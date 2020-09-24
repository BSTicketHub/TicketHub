using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopViewModel
    {   
        public Guid Id { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "請輸入簡介")]
        public string ShopIntro { get; set; }
        [Required(ErrorMessage = "請輸入電話")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        [Required(ErrorMessage = "請輸入地址")]
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BannerImg { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IEnumerable<SimpleIssueViewModel> Issues { get; set; }
    }
}