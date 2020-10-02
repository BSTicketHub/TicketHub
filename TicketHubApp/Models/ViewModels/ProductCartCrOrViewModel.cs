using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class ProductCartCrOrViewModel
    {
        public string UserId { get; set; }
        public List<Guid> id { get; set; }
        public List<int> amount { get; set; }
    }
}