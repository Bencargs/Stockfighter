using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stockfighter.Config;
using Stockfighter.Model;
using Stockfighter.Model.Orders;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.Orders.Request;
using Direction = Stockfighter.Config.Utils.Direction;
using OrderType = Stockfighter.Config.Utils.OrderType;

namespace ModelTest
{
    [TestClass]
    public class PostOrderTest
    {
        [TestMethod]
        public void PostOrderTest_Default()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.AreEqual(Utils.TestVenue, reply.Venue);
            Assert.AreEqual(Utils.TestStock, reply.Symbol);
            Assert.IsNotNull(reply.Fills);
        }

        [TestMethod]
        public void PostOrderTest_Sell()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Sell);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.AreEqual(Utils.TestVenue, reply.Venue);
            Assert.AreEqual(Utils.TestStock, reply.Symbol);
            Assert.IsTrue(reply.Fills.Count > 0);
        }

        [TestMethod]
        public void PostOrderTest_Limit()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.Limit);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.AreEqual(Utils.TestVenue, reply.Venue);
            Assert.AreEqual(Utils.TestStock, reply.Symbol);
        }

        [TestMethod]
        public void PostOrderTest_FillOrKill()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.FillOrKill);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.AreEqual(Utils.TestVenue, reply.Venue);
            Assert.AreEqual(Utils.TestStock, reply.Symbol);
        }

        [TestMethod]
        public void PostOrderTest_Immediate()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.ImmediateOrCancell);
            bool actual = request.Execute();
            Assert.IsTrue(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.AreEqual(Utils.TestVenue, reply.Venue);
            Assert.AreEqual(Utils.TestStock, reply.Symbol);
        }

        [TestMethod]
        public void PostOrderTest_StockDoesNotExist()
        {
            Stock stock = new Stock("", "FALSEEX");
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestVenue, stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNotNull(reply);
            Assert.IsNull(reply.Venue);
            Assert.IsNull(reply.Symbol);
            Assert.IsNull(reply.Fills);
        }

        [TestMethod]
        public void PostOrderTest_VenueDoesNotExist()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 100, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, "FALSEEX", stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains("The remote server returned an error: (404) Not Found.", baseReply.Error);
        }

        [TestMethod]
        public void PostOrderTest_NoVolume()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 0, Direction.Buy);
            PostOrder request = new PostOrder(Utils.TestAccount, Utils.TestStock, stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains("The remote server returned an error: (404) Not Found.", baseReply.Error);
        }

        [TestMethod]
        public void PostOrderTest_NoAccount()
        {
            Stock stock = new Stock("", Utils.TestStock);
            Fill order = new Fill(0, 0, Direction.Buy);
            PostOrder request = new PostOrder("", Utils.TestStock, stock, order, OrderType.Market);
            bool actual = request.Execute();
            Assert.IsFalse(actual);
            OrderReply reply = request.Reply as OrderReply;
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains("The remote server returned an error: (404) Not Found.", baseReply.Error);
        }
    }
}
