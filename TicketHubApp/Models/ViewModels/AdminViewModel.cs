using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketHubApp.PlatformViewModels
{
    public class AdminViewModel
    {
        [Display(Name = "管理者代號")]
        public Guid Id { get; set; }
        [Display(Name = "管理者名稱")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}