using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Memo { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal DiscountRatio { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        [ForeignKey("User")]
        public Guid IssuerId { get; set; }
        public User User { get; set; }
        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }

        public ICollection<IssueTag> IssueTas { get; set; }
    }
}
