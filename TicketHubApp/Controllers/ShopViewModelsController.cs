using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.PlatformViewModels;

namespace TicketHubApp.Controllers
{
    public class ShopViewModelsController : Controller
    {
        private TicketHubPlatformContext db = new TicketHubPlatformContext();

        // GET: ShopViewModels
        public ActionResult Index()
        {
            return View(db.shops.ToList());
        }

        // GET: ShopViewModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopViewModel shopViewModel = db.shops.Find(id);
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
                shopViewModel.Id = Guid.NewGuid();
                db.shops.Add(shopViewModel);
                db.SaveChanges();
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
            ShopViewModel shopViewModel = db.shops.Find(id);
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
            ShopViewModel shopViewModel = db.shops.Find(id);
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
            ShopViewModel shopViewModel = db.shops.Find(id);
            db.shops.Remove(shopViewModel);
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
