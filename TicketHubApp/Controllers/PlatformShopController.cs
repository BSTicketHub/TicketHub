using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubApp.PlatformViewModels;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformShopController : Controller
    {
        private TicketHubContext db = new TicketHubContext();

        // GET: ShopViewModels
        public ActionResult Index()
        {
            return View(db.Shop.ToList());
        }

        [HttpPost]
        public ActionResult SearchByShopName(string ShopName)
        {
            List<Shop> shopList = db.Shop.Where(s => s.ShopName.Contains(ShopName)).ToList();
            return View("Index", shopList);
        }

        [HttpPost]

        public ActionResult SearchByPhone(string Phone)
        {
            List<Shop> shopList = db.Shop.Where(s => s.Phone.Contains(Phone)).ToList();
            return View("Index", shopList);
        }

        [HttpPost]
        public ActionResult SearchByFax(string Fax)
        {
            List<Shop> shopList = db.Shop.Where(s => s.Fax.Contains(Fax)).ToList();
            return View("Index", shopList);
        }

        [HttpPost]
        public ActionResult SearchByWebsite(string Website)
        {
            List<Shop> shopList = db.Shop.Where(s => s.Website.Contains(Website)).ToList();
            return View("Index", shopList);
        }

        // GET: ShopViewModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shopViewModel = db.Shop.Find(id);
            if (shopViewModel == null)
            {
                return HttpNotFound();
            }
            return View(shopViewModel);
        }

        // GET: ShopViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ShopName,Phone,Fax,Address,Email,Website")] ShopViewModel shopViewModel)
        {
            if (ModelState.IsValid)
            {
                //shopViewModel.Id = Guid.NewGuid();
                //db.Shop.Add(shopViewModel);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shopViewModel);
        }

        // GET: ShopViewModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shopViewModel = db.Shop.Find(id);
            if (shopViewModel == null)
            {
                return HttpNotFound();
            }
            return View(shopViewModel);
        }

        // POST: ShopViewModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ShopName,Phone,Fax,Address,Email,Website")] ShopViewModel shopViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopViewModel);
        }

        // GET: ShopViewModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shopViewModel = db.Shop.Find(id);
            if (shopViewModel == null)
            {
                return HttpNotFound();
            }
            return View(shopViewModel);
        }

        // POST: ShopViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Shop shopViewModel = db.Shop.Find(id);
            db.Shop.Remove(shopViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}