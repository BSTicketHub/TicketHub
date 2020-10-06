using System.ComponentModel.DataAnnotations;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopApplyViewModel
    {
        [Required]
        [Display(Name = "商家名稱")]
        public string ShopName { get; set; }
        [Display(Name = "商家介紹")]
        public string ShopIntro { get; set; }
        [Required]
        [Display(Name = "電話")]
        public string Phone { get; set; }
        [Display(Name = "傳真")]
        public string Fax { get; set; }
        [Required]
        [Display(Name = "縣市")]
        public string City { get; set; }
        [Required]
        [Display(Name = "區鄉")]
        public string District { get; set; }
        [Required]
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "郵遞區號")]
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
