using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.Orders.Request
{
    public class GetQuote : BaseRequest
    {
        public GetQuote()
        {
            Url = "https://api.stockfighter.io/ob/api/venues/TESTEX/stocks/FOOBAR/quote";
        }

        public GetQuote(string venue, Stock stock)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/quote", venue, stock.Symbol);
        }

        public override bool Execute()
        {
            return base.Execute<GetQuoteReply>().Result;
        }
    }
}
