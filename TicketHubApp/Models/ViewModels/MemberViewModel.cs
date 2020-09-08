using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketHubApp.PlatformViewModels
{
    public class MemberViewModel
    {   
        [Display(Name = "使用者代號")]
        public Guid Id { get; set; }

        [Display(Name = "使用者帳號")]
        public string Account { get; set; }

        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}