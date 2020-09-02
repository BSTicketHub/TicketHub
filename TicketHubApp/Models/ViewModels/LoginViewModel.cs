using System.ComponentModel.DataAnnotations;

namespace TicketHubApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsSignUp { get; set; }
    }
}
