using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class UserFavoriteShop
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
