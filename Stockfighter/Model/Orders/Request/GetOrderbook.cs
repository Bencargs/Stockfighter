using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.Orders.Request
{
    public class GetOrderbook : BaseRequest
    {
        public GetOrderbook()
        {
            Url = "https://api.stockfighter.io/ob/api/venues/TESTEX/stocks/FOOBAR";
        }

        public GetOrderbook(string venue, Stock stock)
            : this()
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}", venue, stock.Symbol);
        }

        public override bool Execute()
        {
            return base.Execute<GetOrderbookReply>().Result;
        }
    }
}
