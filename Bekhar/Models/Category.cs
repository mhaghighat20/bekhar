using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static List<Category> GetAllCategories()
        {
            var strList = new List<string>()
            {
                "موبایل، تبلت و گجت",
                "لپ تاپ، کامپیوتر و شبکه",
                "ساعت، جواهر و زیورآلات",
                "اشیاء قدیمی و کلکسیونی",
                "قطعات و لوازم ماشین و موتور ",
                "لوازم خانه، آشپزخانه و مبلمان",
                "پوشاک زنانه",
                "پوشاک مردانه",
                "لوازم صوتی و تصویری",
                "دوربین عکاسی و فیلمبرداری",
                "کتاب و لوازم تحریر",
                "اسباب بازی و سرگرمی"
            };

            int counter = 0;
            var result = new List<Category>();

            foreach(var str in strList)
            {
                result.Add(new Category() { Id = counter++, Name = str });
            }

            return result;
        }
    }
}