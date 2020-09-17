using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopViewModel
    {   
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string ShopIntro { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BannerImg { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IEnumerable<SimpleIssueViewModel> Issues { get; set; }
    }
}