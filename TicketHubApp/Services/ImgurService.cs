using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Reflection.Emit;
using TicketHubDataLibrary.Models;
using TicketHubDataLibrary;
using Microsoft.AspNet.Identity;
using System.Runtime.InteropServices;

namespace TicketHubApp.Services
{
    public class ImgurService
    {

        public string UploadImgur(HttpPostedFileBase file)
        {
            var apiClient = new ApiClient("541a79f12e7772b", "c824e682983f55dc1e2642b38741d657d4206db3");
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = imageEndpoint.UploadImageAsync(file.InputStream);

            //return imageUpload.Link;

            var ImgPath = imageUpload.Result.Link;
            return ImgPath;
        }

        public List<string> getSideMenuImage(string role)
        {
            var context = new TicketHubContext();
            var userid = HttpContext.Current.User.Identity.GetUserId();
            string src = null, name = null;
            List<string> result;
            switch (role)
            {
                case PageType.CUSTOMER:
                    src = (from u in context.Users where u.Id == userid select u.AvatarPath).FirstOrDefault();
                    name = (from u in context.Users where u.Id == userid select u.UserName).FirstOrDefault();
                    break;
                case PageType.SHOP:
                    src = (from e in context.ShopEmployee join s in context.Shop on e.ShopId equals s.Id where (e.UserId == userid) select s.BannerImg).FirstOrDefault();
                    name = (from e in context.ShopEmployee join s in context.Shop on e.ShopId equals s.Id where (e.UserId == userid) select s.ShopName).FirstOrDefault();
                    break;
                case PageType.PLATFORM:
                    src = context.Users.Where(x => x.Id == userid).FirstOrDefault().AvatarPath;
                    name = context.Users.Where(x => x.Id == userid).FirstOrDefault().UserName;
                    break;
                default:
                    break;
            }

            src = (src == null) ? "https://i.imgur.com/ZM5EvHg.png" : src;
            name = (name == null) ? "No Name" : name;
            result = new List<string>() { src, name };
            return result;
        }

    }
}