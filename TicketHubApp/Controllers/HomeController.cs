﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.Models.ViewModels;
using TicketHubApp.Services;

namespace TicketHubApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new HomeCardService();
            var homecard = new HomeListViewModel() //新增ViewModel集合
            {
                BestSellerItems = service.GetBestSellerCard(),
                RecommenItems = service.GetRecommenCard(),
                SortNewItems = service.GetSortNewCard(0),
                LimitedtimeItems = service.GetLimitedtimeCard()
            };

            return View(homecard);
        }

        
        [HttpGet]
        // 最新推出 api
        public ActionResult CardApi(int currCount) 
        {
            var service = new HomeCardService();
            var SortNewItems = service.GetSortNewCard(currCount);
            return Json(SortNewItems, JsonRequestBehavior.AllowGet); //把HomeCardService 物件轉JSON，給前端抓資料
        }

        public ActionResult PageUnfound()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchType, string SearchContent)
        {
            if(SearchType == "餐廳")
            {
                return RedirectToRoute("ShopList", new { input = SearchContent });
            } else if(SearchType == "票券")
            {
                return RedirectToRoute("TicketList", new { input = SearchContent });
            }
            return Content("");
        }
    }
}