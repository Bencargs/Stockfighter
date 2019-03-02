using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stockfighter.Model;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.Orders.Request;


namespace ModelTest
{
    [TestClass]
    public class GetStocksTest
    {
        [TestMethod]
        public void GetStockTest_Default()
        {
            GetStocks request = new GetStocks();
            bool actual = request.Execute();
            GetStocksReply reply = request.Reply as GetStocksReply;
            Assert.IsTrue(actual);
            Assert.IsNotNull(reply);
            Assert.IsTrue(string.IsNullOrWhiteSpace(reply.Error));
        }

        [TestMethod]
        public void GetStockTest_VenueExists()
        {
            GetStocks request = new GetStocks("TESTEX");
            bool actual = request.Execute();
            GetStocksReply reply = request.Reply as GetStocksReply;
            Assert.IsTrue(actual);
            Assert.IsNotNull(reply);
            Assert.IsTrue(string.IsNullOrWhiteSpace(reply.Error));
        }

        [TestMethod]
        public void GetStockTest_DoesNotExist()
        {
            GetStocks request = new GetStocks("FALSEEX");
            bool actual = request.Execute();
            GetStocksReply reply = request.Reply as GetStocksReply;
            Assert.IsFalse(actual);
            Assert.IsNull(reply);
            BaseReply baseReply = request.Reply as BaseReply;
            Assert.IsNotNull(baseReply);
            StringAssert.Contains(baseReply.Error, "The remote server returned an error: (404) Not Found");
        }
    }
}
