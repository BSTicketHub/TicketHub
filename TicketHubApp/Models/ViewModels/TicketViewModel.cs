using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Models.ViewModels
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public DateTime DeliveredDate { get; set; }
        public bool? Exchanged { get; set; }
        public string ExchangedDate { get; set; }
        public bool? Voided { get; set; }
        public string VoidedDate { get; set; }
        public Guid IssueId { get; set; }
        public string UserId { get; set; }
        public Guid OrderId { get; set; }
        public string IssueTitle { get; set; }
        public string ImgPath { get; set; }
    }
}