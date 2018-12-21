using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bekhar.Elastic;
using Bekhar.Models;

namespace Bekhar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //List<Kala> kalas = new List<Kala>();
            //foreach (Kala kala in GetSampleKalaData())
            //    kalas.Add(kala);

            var kalaList = ElasticEngine.GetAllKala();

            return View(kalaList);
        }

        [HttpPost]
        public ActionResult Index(string keyword, string category, string city, string location, long priceMin, long priceMax)
        {
            var searchParam = new SearchParameter()
            {
                Keyword = keyword,
                Category = category,
                City = city,
                Location = location,
                PriceMin = priceMin,
                PriceMax = priceMax
            };

            var kalaList = ElasticEngine.GetKalaBySearchParam(searchParam);

            return View(kalaList);
        }

        // Sample Data
        private Kala[] GetSampleKalaData()
        {
            Kala[] kalas = new Kala[5];
            kalas[0] = new Kala() { Id = "0", Name = "جارو برقی در حد", CreationTime = DateTime.Now, Description = "خیلی خوبه!", Mobile = "091221211", Price = 5000 };
            kalas[1] = new Kala() { Id = "1", Name = "جارو برقی عادی", CreationTime = DateTime.Now, Description = "بد نیست ولی خارجیه!", Mobile = "091221211", Price = 43000 };
            kalas[2] = new Kala() { Id = "2", Name = "جارو برقی بد", CreationTime = DateTime.Now, Description = "خیلی خوبه فقط جارو نمیکنه", Mobile = "091221211", Price = 500 };
            kalas[3] = new Kala() { Id = "3", Name = "جارو دستی خوب", CreationTime = DateTime.Now, Description = "از همشون بهتره. فقط یذره ریش ریش شده و بو میده", Mobile = "091221211", Price = 50 };
            kalas[4] = new Kala() { Id = "4", Name = "جارو", CreationTime = DateTime.Now, Description = "انتظار داری چی باشه؟", Mobile = "091221211", Price = 5 };
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