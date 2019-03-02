using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stockfighter.Model;
using Stockfighter.Model.Orders.Request;

namespace ModelTest
{
    [TestClass]
    public class HeartbeatTest
    {
        [TestMethod]
        public void HeartbeatTest_Default()
        {
            GetHeartbeat request = new GetHeartbeat();
            bool actual = request.Execute();
            BaseReply reply = request.Reply as BaseReply;
            Assert.IsTrue(actual);
            Assert.IsNotNull(reply);
            Assert.IsTrue(string.IsNullOrWhiteSpace(reply.Error));
        }

        [TestMethod]
        public void HeartbeatTest_VenueExists()
        {
            GetHeartbeat request = new GetHeartbeat("TESTEX");
            bool actual = request.Execute();
            BaseReply reply = request.Reply as BaseReply;
            Assert.IsTrue(actual);
            Assert.IsNotNull(reply);
            Assert.IsTrue(string.IsNullOrWhiteSpace(reply.Error));
        }

        [TestMethod]
        public void HeartbeatTest_VenueDoesNotExist()
        {
            GetHeartbeat request = new GetHeartbeat("FALSEEX");
            bool actual = request.Execute();
            BaseReply reply = request.Reply as BaseReply;
            Assert.IsFalse(actual);
            Assert.IsNotNull(reply);
            StringAssert.Contains(reply.Error, "The remote server returned an error: (404) Not Found");
        }
    }
}
