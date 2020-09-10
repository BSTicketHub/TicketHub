using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class IssueCategory
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Issue")]
        public Guid IssueId { get; set; }
        public Issue Issue { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
