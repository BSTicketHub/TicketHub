using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class UserWishIssue
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Issue")]
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
