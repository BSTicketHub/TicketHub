using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Refund
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime AppliedDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public TicketHubUser User { get; set; }
        [ForeignKey("RefundId")]
        public ICollection<RefundDetail> RefundDetails { get; set; }
    }
}
