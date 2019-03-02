using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Request
{
    public class GetQuoteAsync : BaseRequestAsync<GetQuoteReply>
    {
        public GetQuoteAsync(string venue, Stock stock)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/quote", venue, stock.Symbol);
        }
    }
}
