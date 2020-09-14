using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Reflection.Emit;

namespace TicketHubApp.Services
{
    public class ImgurService
    {
        //public ImgurService()
        //{
        //    Console.WriteLine("start");
        //    var apiClient = new ApiClient("541a79f12e7772b");
        //    var httpClient = new HttpClient();

        //    var oAuth2Endpoint = new OAuth2Endpoint(apiClient, httpClient);
        //    var authUrl = oAuth2Endpoint.GetAuthorizationUrl();

        //    var token = new OAuth2Token
        //    {
        //        AccessToken = "4eef8487399dffa10a3d7b20058ebd95b6648784",
        //        RefreshToken = "f8de7ef58c55a44b450933028811f5efe477a45a",
        //        AccountId = 138100559,
        //        AccountUsername = "zxc7788991",
        //        ExpiresIn = 315360000,
        //        TokenType = "bearer"
        //    };

        //    apiClient.SetOAuth2Token(token);
        //    var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
        //}

        public string UploadImgur(HttpPostedFileBase file)
        {
            var apiClient = new ApiClient("541a79f12e7772b", "c824e682983f55dc1e2642b38741d657d4206db3");
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = imageEndpoint.UploadImageAsync(file.InputStream);

            var ImgPath = imageUpload.Result.Link;
            return ImgPath;
        }



    }
}