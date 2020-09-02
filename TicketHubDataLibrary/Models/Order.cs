using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderedDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("OrderId")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
