using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Bekhar.Models
{
    public static class Utility
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

        public static Category GetCategoryById(int id)
        {
            return Category.GetAllCategories().FirstOrDefault(x => x.Id == id);
        }

        public static List<Category> GetAllCategories()
        {
            return Category.GetAllCategories().Where(x => x.ParentId == null).ToList();
        }

        public static List<Category> GetCategoryByParentId(int parentId)
        {
            return Category.GetAllCategories().Where(x => x.ParentId == parentId).ToList();
        }

        public static string GetPersianStr(this DateTime dt)
        {
            //if (dt == null)
            //    return null;

            return GetPersianStrFromDT(dt);
        }

        public static string GetPersianStrFromDT(DateTime dt)
        {
            var pc = new PersianCalendar();
            var v = dt;
            return $"{pc.GetYear(v)}-{pc.GetMonth(v)}-{pc.GetDayOfMonth(v)} {v.Hour}:{v.ToString("mm")}";
        }

        public static string GetCommaSeperatedNumber(string num)
        {
            string t = "";
            for (int i = num.Length; i >= 0; i -= 3)
            {
                if (i > 3)
                    t = "," + num.Substring(i - 3, 3) + t;
                else
                    t = num.Substring(0, i) + t;
            }
            return t;
        }

        public static string GetSummery(string txt)
        {
            return txt.Substring(0, Math.Min(60, txt.Length - 1)) + (txt.Length >= 60 ? ". . ." : "");
        }
    }
}