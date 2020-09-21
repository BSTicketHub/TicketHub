using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ProductDetailService
    {
        public ProductDetailViewModel GetIssue(Guid Id)
        {
            TicketHubContext context = new TicketHubContext();
            var issueRepository = new GenericRepository<Issue>(context);
           
            var shopRepository = new GenericRepository<Shop>(context);
            ProductDetailViewModel productItem = new ProductDetailViewModel();
            var issue = issueRepository.GetAll().First((x) => x.Id == Id);
            var shop = shopRepository.GetAll().First((x) => x.Id == issue.ShopId);

            var result = new ProductDetailViewModel
            {
                Id = issue.Id,
                Title = issue.Title,
                Memo = issue.Memo,
                ImgPath = issue.ImgPath,
                OriginalPrice = issue.OriginalPrice,
                DiscountPrice = issue.DiscountPrice,
                ShopName = shop.ShopName,
                ShopIntro = shop.ShopIntro,
                Phone = shop.Phone,
                Fax = shop.Fax,
                District = shop.District,
                Address = shop.Address,
                Email = shop.Email,
                Website = shop.Website,
                ReleasedDate = issue.ReleasedDate,
            };
            return result;
        }
    }
}