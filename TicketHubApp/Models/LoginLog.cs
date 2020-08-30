using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class LoginLog
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime LogedDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("UserId")]
        public Admin Admin { get; set; }
    }
}
