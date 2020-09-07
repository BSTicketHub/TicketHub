using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class CustomerDetailViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set;}
        public string Mobile { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}