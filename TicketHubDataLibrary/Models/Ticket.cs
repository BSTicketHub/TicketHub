using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime DeliveredDate { get; set; }
        public bool Exchanged { get; set; }
        public DateTime ExchangedDate { get; set; }
        public bool Voided { get; set; }
        public DateTime VoidedDate { get; set; }

        public Guid IssueId { get; set; }
        [ForeignKey("IssueId")]
        public Issue Issue { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
