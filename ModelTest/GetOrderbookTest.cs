using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stockfighter.Model;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.Orders.Request;
using Stockfighter.Model.WebSocket.Request;

namespace ModelTest
{
    [TestClass]
    public class GetOrderbookTest
    {
        [TestMethod]
        public void GetOrderbookTest_Default()
        {
            GetOrderbook request = new GetOrderbook();
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            GetOrderbookReply reply = request.Reply as GetOrderbookReply;
            Assert.IsNotNull(reply);
        }

        [TestMethod]
        public void GetOrderbookTest_VenueAndStockExists()
        {
            Stock stock = new Stock("", "FOOBAR");
            GetOrderbook request = new GetOrderbook("TESTEX", stock);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            GetOrderbookReply reply = request.Reply as GetOrderbookReply;
            Assert.IsNotNull(reply);
        }

        [TestMethod]
        public void GetOrderbookTest_StockDoesNotExists()
        {
            Stock stock = new Stock("", "FALSE");
            GetOrderbook request = new GetOrderbook("TESTEX", stock);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            GetOrderbookReply reply = request.Reply as GetOrderbookReply;
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains(baseReply.Error, "The remote server returned an error: (500) Internal Server Error.");
        }

        [TestMethod]
        public void GetOrderbookTest_VenueAndStockDoesNotExists()
        {
            Stock stock = new Stock("", "FALSE");
            GetOrderbook request = new GetOrderbook("FALSEEX", stock);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            GetOrderbookReply reply = request.Reply as GetOrderbookReply;
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains(baseReply.Error, "The remote server returned an error: (404) Not Found.");
        }

        [TestMethod]
        public void test()
        {
            var req = new GetOrderbookAsync("TESTEX", new Stock("", "FOOBAR"));
            req.ExecuteComplete += (sender, reply) => Console.WriteLine(reply);
            req.Execute();

            Thread.Sleep(10000);
        }
    }
}
