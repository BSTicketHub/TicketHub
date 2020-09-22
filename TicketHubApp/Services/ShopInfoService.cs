using Microsoft.AspNet.Identity;
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
        private TicketHubContext _context = new TicketHubContext();

        private readonly string _userid = HttpContext.Current.User.Identity.GetUserId();

        public ShopViewModel GetShopInfo()
        {
            var shopRepo = new GenericRepository<Shop>(_context);
            var employeeRepo = new GenericRepository<ShopEmployee>(_context);

            Guid shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;
            var shopData = shopRepo.GetAll().FirstOrDefault((x) => x.Id == shopid);

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
            };
            return shopVM;
        }

        public OperationResult UpdateShopInfo(ShopViewModel input)
        {
            var result = new OperationResult();
            try
            {
                var shopRepo = new GenericRepository<Shop>(_context);
                var employeeRepo = new GenericRepository<ShopEmployee>(_context);
                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                Guid shopid = _context.ShopEmployee.FirstOrDefault((x) => x.UserId == _userid).ShopId;

                var entity = new Shop
                {
                    Id = shopid,
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
                _context.SaveChanges();
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