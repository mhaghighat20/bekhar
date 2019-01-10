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
            var cat = Request.QueryString["cat"];
            var city = Request.QueryString["city"];
            var searchParameter = new SearchParameter();

            if (!string.IsNullOrWhiteSpace(cat))
                searchParameter.Category = cat;

            if (!string.IsNullOrEmpty(city))
                searchParameter.City = city;

            var kalaList = ElasticEngine.GetKalaBySearchParam(searchParameter);

            return View(kalaList);
        }

        [HttpPost]
        public ActionResult Index(string keyword, string location, long? priceMin, long? priceMax, string city)
        {
            var searchParameter = new SearchParameter()
            {
                Keyword = keyword,
                Location = location,
                PriceMin = priceMin,
                PriceMax = priceMax
            };

            ViewBag.keyword = keyword;
            ViewBag.location = location;
            ViewBag.city = city;
            ViewBag.priceMin = priceMin;
            ViewBag.priceMax = priceMax;

            var cat = Request.QueryString["cat"];

            if (!string.IsNullOrWhiteSpace(cat))
                searchParameter.Category = cat;

            if (!string.IsNullOrEmpty(city))
                searchParameter.City = city;

            var kalaList = ElasticEngine.GetKalaBySearchParam(searchParameter);

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