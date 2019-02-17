using Bekhar.Controllers;
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
        public long? MockedUserMoney = 1;

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


        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DoInvalidBuy()
        {
            Kharid kharid;
            Transaction transaction;
            var controller = new KharidsController();

            var kala = new Kala()
            {
                Username = "testUser",
                Price = 10,
            };

            controller.GenerateKharid("id5", kala, out kharid, out transaction, "testUser2", MockedUserMoney);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void DoInvalidBid()
        {
            Kharid kharid;
            Transaction transaction;
            var controller = new KharidsController();

            var kala1 = new Kala()
            {
                Username = "testUser",
                Price = 10,
                DataType = ModelType.Mozayede
            };

            var kala2 = new Kala()
            {
                Username = "testUser",
                Price = 10,
                DataType = ModelType.Mozayede
            };

            controller.GenerateKharid("id5", kala1, out kharid, out transaction, "testUser2", MockedUserMoney);
            controller.GenerateKharid("id5", kala2, out kharid, out transaction, "testUser2", MockedUserMoney - kala1.Price.Value);
        }

        [TestMethod]
        public void DoValidBid()
        {
            Kharid kharid;
            Transaction transaction;
            var controller = new KharidsController();

            var kala1 = new Kala()
            {
                Username = "testUser",
                Price = 10,
                DataType = ModelType.Mozayede
            };

            controller.GenerateKharid("id5", kala1, out kharid, out transaction, "testUser2", MockedUserMoney);
        }

        [TestMethod]
        public void DoValidBuy()
        {
            Kharid kharid;
            Transaction transaction;
            var controller = new KharidsController();

            var kala1 = new Kala()
            {
                Username = "testUser",
                Price = 1,
                DataType = ModelType.Mozayede
            };

            controller.GenerateKharid("id5", kala1, out kharid, out transaction, "testUser2", MockedUserMoney);
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
