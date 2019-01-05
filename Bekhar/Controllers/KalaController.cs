using Bekhar.Elastic;
using Bekhar.Models;
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
            Kala item = ElasticEngine.GetKalaById(id);
            // Kala item = GetSampleKala(id);

            return View(item);
        }

        // GET: Kala/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kala/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Location,City,Category")] Kala kala)
        {
            if (ModelState.IsValid)
            {
                //var Username = User.Identity.Name;
                kala.Username = User.Identity.Name;
                //var userId = _core.FindUserId(UserName, false);
                //var newProject = project;
                kala.CreationTime = DateTime.Now;
                ElasticEngine.AddKala(kala);
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kala/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Kala item = ElasticEngine.GetKalaById(id);
                if (item.Username == User.Identity.Name)
                    ElasticEngine.DeleteKala(item.Id);
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
