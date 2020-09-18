using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class CustomerOrderViewModel
    {
        public Guid Id { get; set; }
        public string OrderDate { get; set; }
        public string UserId { get; set; }
        public IEnumerable<ShopIssueViewModel> OrderIssue { get; set; }
    }
}