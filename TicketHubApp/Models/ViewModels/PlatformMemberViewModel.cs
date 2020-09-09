using System.ComponentModel.DataAnnotations;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformMemberViewModel
    {
        [Display(Name = "使用者代號")]
        public string Id { get; set; }
        [Display(Name = "使用者名稱")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}