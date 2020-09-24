using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class IssueListSearchViewModel
    {
        public List<string> SelectedTag { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
}