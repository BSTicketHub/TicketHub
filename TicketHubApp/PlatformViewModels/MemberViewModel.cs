using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHubApp.PlatformViewModels
{
    public class MemberViewModel
    {   
        [Key]
        public int Id { get; set; }
        [DisplayName("帳號")]
        public string Account { get; set; }
        public string Password { get; set; }
        [DisplayName("會員名稱")]
        public string Name { get; set; }
        [DisplayName("電子郵件")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("行動電話")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
    }
}