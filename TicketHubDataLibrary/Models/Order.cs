using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime OrderedDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public TicketHubUser User { get; set; }

        [ForeignKey("OrderId")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
