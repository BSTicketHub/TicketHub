using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class ShopCategory
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
