using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class ShopTag
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
