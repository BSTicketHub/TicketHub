using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformEmployeeViewModel
    {
        [Display(Name = "員工代號")]
        public string Id { get; set; }

        [Display(Name = "員工名稱")]
        public string UserName { get; set; }
        [Display(Name = "員工電子郵件")]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}