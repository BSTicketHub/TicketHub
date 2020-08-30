using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order=1)]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid IssueId { get; set; }
        [ForeignKey("IssueId")]
        public Issue Issue { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
