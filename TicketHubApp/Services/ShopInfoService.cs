using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using TicketHubApp.Models;
using TicketHubApp.Models.ViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Services
{
    public class ShopInfoService
    {
        string apiKey = ConfigurationManager.AppSettings["googleMapApiKey"].ToString();
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

        public OperationResult UpdateShopInfo(ShopViewModel input, List<string> coordinates)
        {
            var result = new OperationResult();
            try
            {
                var shopRepo = new GenericRepository<Shop>(_context);
                var employeeRepo = new GenericRepository<ShopEmployee>(_context);
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
                    Lat = coordinates[0],
                    Lng = coordinates[1],
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

        public List<string> geocodeLatLng(string address)
        {
            List<string> coordinates;
            string Json;

            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}&callback";

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                Json = reader.ReadToEnd();
            }
            response.Close();

            var dat = Newtonsoft.Json.JsonConvert.DeserializeObject<JToken>(Json);
            var result = dat["results"][0]["geometry"]["location"];
            coordinates = new List<string>() { result["lat"].ToString(), result["lng"].ToString() };

            return coordinates;
        }
    }
}