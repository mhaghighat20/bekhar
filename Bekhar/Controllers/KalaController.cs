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
    [Authorize]
    public class KalaController : Controller
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

        // GET: Kala
        public ActionResult Index()
        {
            var searchParam = new SearchParameter()
            {
                Username = User.Identity.Name
            };

            List<Kala> items = ElasticEngine.GetKalaBySearchParam(searchParam);
            return View(items);
        }

        // GET: Kala/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            Kala item = GetKala(id, UserManager);

            return View(item);
        }

        public static Kala GetKala(string id, ApplicationUserManager userManager)
        {
            Kala item = ElasticEngine.GetKalaById(id);

            if (int.TryParse(item.Category, out var catId))
                item.Category = Utility.GetCategoryById(catId).Name;

            var user = userManager.FindByNameAsync(item.Username).Result;
            item.Mobile = user.PhoneNumber;
            item.Email = user.Email;
            item.Id = id;
            return item;
        }

        // GET: Kala/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kala/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Location,City,Category,DeadlineDate,DeadlineTime")] Kala kala)
        {
            // TODO ولیدیشن ها چک شوند
            //if (ModelState.IsValid)
            {
                kala.Username = User.Identity.Name;
                kala.CreationTime = DateTime.Now;

                if (string.IsNullOrWhiteSpace(kala.DeadlineDate))
                    ElasticEngine.AddKala(kala);
                else
                {
                    kala.DataType = ModelType.Mozayede;
                    kala.Deadline = Convert.ToDateTime(kala.DeadlineDate);
                    if (!string.IsNullOrWhiteSpace(kala.DeadlineTime))
                    {
                        var time = kala.DeadlineTime.Split(':').Select(x => Convert.ToInt32(x)).ToList();
                        kala.Deadline.Add(new TimeSpan(time[0], time[1], 0));
                    }

                    ElasticEngine.AddKala(kala);
                }
                return RedirectToAction("Index", "Home", null);
            }

            return View();
        }

        // GET: Kala/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kala/Edit/5
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

        // GET: Kala/Delete/5
        public ActionResult Delete(string id)
        {
            Kala item = ElasticEngine.GetKalaById(id);

            if (int.TryParse(item.Category, out var catId))
                item.Category = Utility.GetCategoryById(catId).Name;

            return View(item);
        }

        // POST: Kala/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Kala item = ElasticEngine.GetKalaById(id);
                if (item.Username == User.Identity.Name)
                    ElasticEngine.DeleteKala(id);
                else
                    throw new InvalidOperationException("Access is deneied");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
