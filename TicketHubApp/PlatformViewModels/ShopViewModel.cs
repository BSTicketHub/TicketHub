using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketHubApp.PlatformViewModels
{
    public class ShopViewModel
    {   
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}