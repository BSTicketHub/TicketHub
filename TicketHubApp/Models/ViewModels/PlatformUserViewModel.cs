using System.ComponentModel.DataAnnotations;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformUserViewModel
    {
        [Display(Name = "使用者代號")]
        public string Id { get; set; }
        [Display(Name = "使用者帳號")]
        public string UserAccount { get; set; }
        [Display(Name = "密碼")]
        public string PasswordHash { get; set; }
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }
    }
}