using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Refund
    {
        public Guid Id { get; set; }
        public DateTime AppliedDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RefundId")]
        public ICollection<RefundDetail> RefundDetails { get; set; }
    }
}
