using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Utility
    {
        public List<string> GetAllCities()
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
    }
}