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