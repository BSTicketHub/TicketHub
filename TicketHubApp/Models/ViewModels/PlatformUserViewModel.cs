using System.ComponentModel.DataAnnotations;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformUserViewModel
    {
        [Required]
        [Display(Name = "使用者代號")]
        public string Id { get; set; }
        [Display(Name = "使用者帳號")]
        [EmailAddress]
        [Required]
        public string UserAccount { get; set; }
        [Display(Name = "手機號碼")]
        [Required(ErrorMessage = "請填入手機號碼 !")]
        [RegularExpression(@"^\d{4}[-\s]?\d{3}[-\s]?\d{3}$", ErrorMessage = "手機號碼格式須為 09xx-xxx-xxx")]
        public string Mobile { get; set; }
        [Display(Name = "註銷狀態")]
        [Required]
        public bool Deleted { get; set; }
        [Display(Name = "鎖定狀態")]
        [Required]
        public bool Locked { get; set; }
        public string AvatarPath { get; set; }
        [Required]
        [Display(Name = "性別")]
        public string Sex { get; set; }
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

    }
}