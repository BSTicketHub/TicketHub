namespace TicketHubApp.Models.ServiceModels
{
    public class UserToken
    {
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int PasswordWorkFactor { get; set; }
    }
}
