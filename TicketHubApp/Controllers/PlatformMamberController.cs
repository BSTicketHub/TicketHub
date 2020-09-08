using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TicketHubDataLibrary.Models;

namespace TicketHubApp.Controllers
{
    public class PlatformMamberController : Controller
    {
        private TicketHubContext db = new TicketHubContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        [HttpPost]
        public ActionResult SearchByUserName(string UserName)
        {
            List<User> memberList = db.User.Where(m => m.UserName.Contains(UserName)).ToList();

            return View("Index", memberList);
        }
        [HttpPost]
        public ActionResult SearchByAccount(string Account)
        {
            List<User> memberList = db.User.Where(m => m.Email.Contains(Account)).ToList();

            return View("Index", memberList);
        }

        [HttpPost]
        public ActionResult SearchByMobile(string Mobile)
        {
            List<User> memberList = db.User.Where(m => m.Mobile.Contains(Mobile)).ToList();

            return View("Index", memberList);
        }

        [HttpPost]
        public ActionResult SearchByEmail(string Email)
        {
            List<User> memberList = db.User.Where(m => m.Email.Contains(Email)).ToList();

            return View("Index", memberList);
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User memberViewModel = db.User.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,UserName,Mobile,Email")] User memberViewModel)
        {
            if (ModelState.IsValid)
            {
                memberViewModel.Id = Guid.NewGuid();
                db.User.Add(memberViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberViewModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User memberViewModel = db.User.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // POST: Users/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,UserName,Mobile,Email")] User memberViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberViewModel);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User memberViewModel = db.User.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User memberViewModel = db.User.Find(id);
            db.User.Remove(memberViewModel);
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
