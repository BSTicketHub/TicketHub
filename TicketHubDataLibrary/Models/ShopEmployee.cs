using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class ShopEmployee
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public TicketHubUser User { get; set; }
    }
}
