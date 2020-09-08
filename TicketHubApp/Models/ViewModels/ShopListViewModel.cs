using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class ShopListViewModel
    {
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string ShopIntro { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string OpenTime { get; set; }

        public List<IssueDetailViewModel> issues { get; set; }

        //ForIssue


    }
}