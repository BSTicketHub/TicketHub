using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopIssueDetailViewModel
    {
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
        public int OrderAmount { get; set; }
        public int UsedAmount { get; set; }
        public int ReleaseAmount { get; set; }
        public string UserPhone { get; set; }
    }
}