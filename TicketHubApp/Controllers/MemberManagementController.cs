using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using TicketHubApp.PlatformViewModels;

namespace TicketHubApp.Controllers
{
    public class MemberManagementController : Controller
    {
        private TicketHubPlatformContext db = new TicketHubPlatformContext();

        // GET: MemberViewModels
        public ActionResult Index()
        {
            return View(db.members.ToList());
        }

        [HttpPost]
        public ActionResult SearchByUserName(string UserName)
        {   
            List<MemberViewModel> memberList = db.members.Where(m => m.UserName.Contains(UserName)).ToList();

            return View("Index", memberList);
        }
        [HttpPost]
        public ActionResult SearchByAccount(string Account)
        {
            List<MemberViewModel> memberList = db.members.Where(m => m.Account.Contains(Account)).ToList();

            return View("Index", memberList);
        }

        [HttpPost]
        public ActionResult SearchByMobile(string Mobile)
        {
            List<MemberViewModel> memberList = db.members.Where(m => m.Mobile.Contains(Mobile)).ToList();

            return View("Index", memberList);
        }

        [HttpPost]
        public ActionResult SearchByEmail(string Email)
        {
            List<MemberViewModel> memberList = db.members.Where(m => m.Email.Contains(Email)).ToList();

            return View("Index", memberList);
        }

        // GET: MemberViewModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.members.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // GET: MemberViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,UserName,Mobile,Email")] MemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                memberViewModel.Id = Guid.NewGuid();
                db.members.Add(memberViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberViewModel);
        }

        // GET: MemberViewModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.members.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // POST: MemberViewModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,UserName,Mobile,Email")] MemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberViewModel);
        }

        // GET: MemberViewModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.members.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // POST: MemberViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MemberViewModel memberViewModel = db.members.Find(id);
            db.members.Remove(memberViewModel);
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
