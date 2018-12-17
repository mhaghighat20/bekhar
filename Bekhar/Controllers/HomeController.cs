using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bekhar.Models;

namespace Bekhar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Kala> kalas = new List<Kala>();
            foreach (Kala kala in GetSampleKalaData())
                kalas.Add(kala);

            // TODO: Get actual kalas

            return View(kalas);
        }

        // Sample Data
        private Kala[] GetSampleKalaData()
        {
            Kala[] kalas = new Kala[5];
            kalas[0] = new Kala() { Name = "جارو برقی در حد", CreationTime = DateTime.Now, Description = "خیلی خوبه!", Mobile = "091221211", Price = 5000 };
            kalas[1] = new Kala() { Name = "جارو برقی عادی", CreationTime = DateTime.Now, Description = "بد نیست ولی خارجیه!", Mobile = "091221211", Price = 43000 };
            kalas[2] = new Kala() { Name = "جارو برقی بد", CreationTime = DateTime.Now, Description = "خیلی خوبه فقط جارو نمیکنه", Mobile = "091221211", Price = 500 };
            kalas[3] = new Kala() { Name = "جارو دستی خوب", CreationTime = DateTime.Now, Description = "از همشون بهتره. فقط یذره ریش ریش شده و بو میده", Mobile = "091221211", Price = 50 };
            kalas[4] = new Kala() { Name = "جارو", CreationTime = DateTime.Now, Description = "انتظار داری چی باشه؟", Mobile = "091221211", Price = 5 };
            return kalas;
        }

        public ActionResult About()
        {
            ViewBag.Message = "I'm testing code :))";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}