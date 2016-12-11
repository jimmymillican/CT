using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClubMembership.DAL;
using ClubMembership.Models;

namespace ClubMembership.Controllers
{
    public class EditionController : Controller
    {
        private MembershipContext db = new MembershipContext();

        // GET: Edition
        public ActionResult Index()
        {
            return View(db.Editions.ToList());
        }

        // GET: Edition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // GET: Edition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionId,Title,Description")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Editions.Add(edition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(edition);
        }

        // GET: Edition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Edition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionId,Title,Description")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(edition);
        }

        // GET: Edition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Edition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edition edition = db.Editions.Find(id);
            db.Editions.Remove(edition);
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
