using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class IssueTag
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Issue")]
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
