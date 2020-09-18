using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopInfoService
    {
        public ShopViewModel GetShopInfo()
        {
            TicketHubContext context = new TicketHubContext();
            var shopRepo = new GenericRepository<Shop>(context);
            var employeeRepo = new GenericRepository<ShopEmployee>(context);
            //var user = HttpContext.Current.User.Identity.GetUserId();
            var user = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4";
            var shopId = employeeRepo.GetAll().Where((x) => x.UserId == user).FirstOrDefault().ShopId;
            var shopData = shopRepo.GetAll().Where((x) => x.Id == shopId).FirstOrDefault();

            var shopVM = new ShopViewModel()
            {
                Id = shopData.Id,
                ShopName = shopData.ShopName,
                ShopIntro = shopData.ShopIntro,
                Phone = shopData.Phone,
                Fax = shopData.Fax,
                City = shopData.City,
                District = shopData.District,
                Address = shopData.Address,
                Email = shopData.Email,
                Website = shopData.Website,
                BannerImg = shopData.BannerImg,
                //Issues = 
            };
            return shopVM;
        }

        public OperationResult UpdateShopInfo(ShopViewModel input)
        {
            var result = new OperationResult();
            try
            {
                TicketHubContext context = new TicketHubContext();
                var shopRepo = new GenericRepository<Shop>(context);
                var employeeRepo = new GenericRepository<ShopEmployee>(context);
                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var user = "26c751ea-d1ce-45bf-8a65-78f0d48ce2c4";
                var shopId = employeeRepo.GetAll().Where((x) => x.UserId == user).FirstOrDefault().ShopId;

                var entity = new Shop
                {
                    Id = shopId,
                    ShopName = input.ShopName,
                    ShopIntro = input.ShopIntro,
                    Phone = input.Phone,
                    Fax = input.Fax,
                    City = input.City,
                    District = input.District,
                    Address = input.Address,
                    //Zip
                    Email = input.Email,
                    Website = input.Website,
                    BannerImg = input.BannerImg,
                    ModifiedDate = DateTime.Now
                };

                if (input.ImgFile != null)
                {
                    var imgurService = new ImgurService();
                    entity.BannerImg = imgurService.UploadImgur(input.ImgFile);
                }
                else
                {
                    entity.BannerImg = input.BannerImg;
                }

                shopRepo.Update(entity);
                context.SaveChanges();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.ToString();
            }
            return result;
        }
    }
}