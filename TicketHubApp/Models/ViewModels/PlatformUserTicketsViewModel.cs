using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class PlatformUserTicketsViewModel
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ShopName { get; set; }
        public DateTime DeliveredDate { get; set; }
        public bool? Exchanged { get; set; }
        public DateTime? ExchangedDate { get; set; }
        public bool? Voided { get; set; }
        public DateTime? VoidedDate { get; set; }
    }
}