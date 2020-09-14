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