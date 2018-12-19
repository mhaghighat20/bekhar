using Bekhar.Elastic;
using Bekhar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bekhar.Controllers
{
    public class KalaController : Controller
    {
        // GET: Kala
        public ActionResult Index()
        {
            List<Kala> items = ElasticEngine.GetAllKala();
            return View(items);
        }

        // GET: Kala/Details/5
        public ActionResult Details(string id)
        {
            Kala item = ElasticEngine.GetKalaById(id);
            // Kala item = GetSampleKala(id);

            return View(item);
        }

        private Kala GetSampleKala(string id)
        {
            Kala[] kalas = new Kala[5];
            kalas[0] = new Kala() { Name = "جارو برقی در حد", CreationTime = DateTime.Now, Description = "خیلی خوبه!", Mobile = "091221211", Price = 5000 };
            kalas[1] = new Kala() { Name = "جارو برقی عادی", CreationTime = DateTime.Now, Description = "بد نیست ولی خارجیه!", Mobile = "091221211", Price = 43000 };
            kalas[2] = new Kala() { Name = "جارو برقی بد", CreationTime = DateTime.Now, Description = "خیلی خوبه فقط جارو نمیکنه", Mobile = "091221211", Price = 500 };
            kalas[3] = new Kala() { Name = "جارو دستی خوب", CreationTime = DateTime.Now, Description = "از همشون بهتره. فقط یذره ریش ریش شده و بو میده", Mobile = "091221211", Price = 50 };
            kalas[4] = new Kala() { Name = "جارو", CreationTime = DateTime.Now, Description = "انتظار داری چی باشه؟", Mobile = "091221211", Price = 5 };

            return kalas[int.Parse(id)];
        }

        // GET: Kala/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kala/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Mobile,Email")] Kala kala)
        {

            if (ModelState.IsValid)
            {
                //var UserName = User.Identity.Name;
                //var userId = _core.FindUserId(UserName, false);
                //var newProject = project;
                kala.CreationTime = DateTime.Now;
                ElasticEngine.AddKala(kala);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kala/Delete/5
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
