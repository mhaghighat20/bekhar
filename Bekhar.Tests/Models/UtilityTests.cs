using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bekhar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bekhar.Models.Tests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void GetAllCitiesTest()
        {
            var cities = Utility.GetAllCities();
            Assert.AreEqual("تهران", cities.First());
            Assert.AreEqual("همدان", cities.Last());
        }

        [TestMethod]
        public void GetAllCategoriesTest()
        {
            var cats = Utility.GetAllCategories();
            Assert.IsFalse(cats.Any(x => x.ParentId != null));
        }

        [TestMethod]
        public void GetParentCategoriesTest()
        {
            var cats = Utility.GetCategoryByParentId(0);
            Assert.IsTrue(cats.All(x => x.ParentId != null));
            Assert.IsFalse(cats.Any(x => x.ParentId == null));
        }
    }
}