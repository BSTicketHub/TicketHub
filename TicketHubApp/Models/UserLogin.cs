using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class UserLogin
    {
        [Key]
        [Column(Order = 1)]
        public string LoginProvide { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ProviderKey { get; set; }
        [Key]
        [Column(Order = 3)]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
