using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bekhar.Models;

namespace Bekhar.Controllers
{
    public class KharidsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kharids
        public ActionResult Index()
        {
            return View(db.Kharids.ToList());
        }

        // GET: Kharids/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kharid kharid = db.Kharids.Find(id);
            if (kharid == null)
            {
                return HttpNotFound();
            }
            return View(kharid);
        }

        // GET: Kharids/Create
        public ActionResult Create(string kalaId)
        {
            if (string.IsNullOrWhiteSpace(kalaId))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kala = new KalaController().GetKala(kalaId);
            return View();
        }

        // POST: Kharids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KalaId,KharidarUsername,ForoshandeUsername,State,DataType")] Kharid kharid)
        {
            if (ModelState.IsValid)
            {
                db.Kharids.Add(kharid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kharid);
        }

        // GET: Kharids/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kharid kharid = db.Kharids.Find(id);
            if (kharid == null)
            {
                return HttpNotFound();
            }
            return View(kharid);
        }

        // POST: Kharids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KalaId,KharidarUsername,ForoshandeUsername,State,DataType")] Kharid kharid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kharid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kharid);
        }

        // GET: Kharids/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kharid kharid = db.Kharids.Find(id);
            if (kharid == null)
            {
                return HttpNotFound();
            }
            return View(kharid);
        }

        // POST: Kharids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kharid kharid = db.Kharids.Find(id);
            db.Kharids.Remove(kharid);
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
