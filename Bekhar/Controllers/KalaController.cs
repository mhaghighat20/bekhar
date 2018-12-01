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
            return View();
        }

        // GET: Kala/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

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
