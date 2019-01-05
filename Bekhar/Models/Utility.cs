using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Utility
    {
        public static List<string> GetAllCities()
        {
            return new List<string>()
            {
                "تهران",
                "کرج",
                "مشهد",
                "اصفهان",
                "تبریز",
                "شیراز",
                "اهواز",
                "قم",
                "کرمانشاه",
                "ارومیه",
                "زاهدان",
                "رشت",
                "کرمان",
                "همدان",
            };
        }

        public static List<Category> GetAllCategories()
        {
            return Category.GetAllCategories();
        }
    }
}