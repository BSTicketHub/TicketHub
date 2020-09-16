using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketHubApp.Models.ViewModels
{
    public class CustomerInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public IEnumerable<ShopViewModel> FavoriteShop { get; set; }
        public IEnumerable<ShopIssueViewModel> WishIssue { get; set; }


    }
}