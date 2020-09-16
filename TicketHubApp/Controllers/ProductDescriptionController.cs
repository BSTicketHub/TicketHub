using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Services;
using TicketHubApp.Models.ViewModels;
using System.Net;

namespace TicketHubApp.Controllers
{
    public class ProductDescriptionController : Controller
    {
        // GET: ticketDescription
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductDescription(string id)
        {
            var service = new ProductDetailService();
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            ProductDetailViewModel productDetailVM = service.GetIssue(Guid.Parse(id));
            
            return View(productDetailVM);
        }
    }
    
}