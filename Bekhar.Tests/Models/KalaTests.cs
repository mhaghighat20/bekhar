using Bekhar.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bekhar.Tests.Models
{
    [TestClass]
    public class KalaTests
    {
        [TestMethod]
        public void NotBidTest()
        {
            Kala kala = GenerateKala();

            Assert.IsFalse(kala.DataType == ModelType.Mozayede);
        }

        [TestMethod]
        public void BidTest()
        {
            var kala = GenerateKala(DateTime.Now.Add(new TimeSpan(2, 0, 0)));

            Assert.IsTrue(kala.DataType == ModelType.Mozayede);
            Assert.IsNotNull(kala.Deadline);
        }


        private static Kala GenerateKala(DateTime? deadline = null)
        {
            var kala = new Kala()
            {
                Name = "کالای تست",
                Price = 50000,
                Deadline = deadline,
            };

            if (deadline.HasValue)
                kala.DataType = ModelType.Mozayede;

            return kala;
        }
    }
}
