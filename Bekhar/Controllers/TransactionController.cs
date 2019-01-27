using Bekhar.Elastic;
using Bekhar.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bekhar.Controllers
{
    public class TransactionController : Controller
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

        // GET: Transaction
        [Authorize]
        public ActionResult Index()
        {
            List<Transaction> items = ElasticEngine.GetTransactionByUsername(User.Identity.Name);
            return View(items);
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Transaction/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transaction/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Amount")] Transaction transaction)
        {
            try
            {
                transaction.Id = null;
                transaction.Username = User.Identity.Name;
                transaction.CreationTime = DateTime.Now;

                if (transaction.Amount <= 0)
                    throw new InvalidCastException("Invalid amount");

                ElasticEngine.AddTranasction(transaction);

                var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
                user.Money += transaction.Amount;
                UserManager.UpdateAsync(user).Wait();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
