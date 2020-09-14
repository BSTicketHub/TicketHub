using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopIssueListViewModel
    {
        public List<ShopIssueViewModel> Items { get; set; }
    }
}