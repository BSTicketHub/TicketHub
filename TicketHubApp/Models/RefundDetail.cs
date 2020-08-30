using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class RefundDetail
    {
        [Key]
        [Column(Order = 1)]
        public Guid RefundId { get; set; }
        [ForeignKey("RefundId")]
        public Refund Refund { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
        public decimal Price { get; set; }
        public bool Refunded { get; set; }
        public DateTime RefundedDate { get; set; }
    }
}
