using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class UserRole
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
