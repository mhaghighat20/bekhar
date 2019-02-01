using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bekhar.Elastic;
using Bekhar.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Bekhar.Controllers
{
    [Authorize]
    public class KharidsController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Kharids
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var items = ElasticEngine.GetKharidByUserName(username);

            foreach (var item in items)
                item.canBeApproved = CanBeApproved(username, item);

            foreach (var item in items)
                item.buyOrSell = item.KharidarUsername == username;

            return View(items);
        }

        private static bool CanBeApproved(string username, Kharid item)
        {
            if (item.KharidarUsername == username)
                if (item.State == KharidState.WaitForApprove)
                    return true;

            return false;
        }

        // GET: Kharids/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kharid kharid = null; // db.Kharids.Find(id);
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

            var kala = KalaController.GetKala(kalaId, UserManager);
            // TODO عدم نمایش کالای خریداری شده
            return View(kala);
        }

        // POST: Kharids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            var kalaId = Request.QueryString["kalaId"];
            var kala = KalaController.GetKala(kalaId, UserManager);
            var money = GetUserMoney(User.Identity.Name);

            if (kala.Price > money)
                return RedirectToAction("Create", "Transaction", null);

            var kharid = new Kharid()
            {
                ForoshandeUsername = kala.Username,
                KharidarUsername = User.Identity.Name,
                KalaId = kalaId,
                State = KharidState.WaitForApprove,
                CreationTime = DateTime.Now
            };

            var transaction = new Transaction()
            {
                Amount = -kala.Price.Value,
                Purpose = PurposeType.Buy,
                Username = User.Identity.Name,
                CreationTime = DateTime.Now,
            };

            ChangeUserMoney(User.Identity.Name, kala.Price.Value, false);
            ElasticEngine.AddKharid(kharid);
            ElasticEngine.AddTranasction(transaction);

            return RedirectToAction("Index");
        }

        private long GetUserMoney(string userName)
        {
            return UserManager.FindByNameAsync(userName).Result.Money;
        }

        public static void ChangeUserMoney(string username, long amount, bool increaseOrDecrease)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);

            var user = manager.FindByNameAsync(username).Result;
            if (increaseOrDecrease)
                user.Money += amount;
            else
                user.Money -= amount;

            manager.UpdateAsync(user);
            //var ctx = store.Context;
            //ctx.SaveChanges();
        }

        // GET: Kharids/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Kharid kharid = ElasticEngine.GetKharidById(id);
            if (kharid == null)
                return HttpNotFound();

            if (!CanBeApproved(User.Identity.Name, kharid))
                throw new InvalidOperationException("در خواست نامعتبر");

            ElasticEngine.SetApprovedKharid(id);

            return RedirectToAction("Index");
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
                //db.Entry(kharid).State = EntityState.Modified;
                //db.SaveChanges();
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
            Kharid kharid = null; //db.Kharids.Find(id);
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
            //Kharid kharid = db.Kharids.Find(id);
            //db.Kharids.Remove(kharid);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
