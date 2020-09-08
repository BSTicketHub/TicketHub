namespace TicketHubApp.Services.Models
{
    public class UserToken
    {
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int PasswordWorkFactor { get; set; }
    }
}
