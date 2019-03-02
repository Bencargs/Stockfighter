using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.Orders.Request
{
    public class GetStocks : BaseRequest
    {
        public GetStocks()
        {
            Url = "https://api.stockfighter.io/ob/api/venues/TESTEX/stocks";
        }

        public GetStocks(string venue)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks", venue);
        }

        public override bool Execute()
        {
            return Execute<GetStocksReply>().Result;
        }
    }
}
