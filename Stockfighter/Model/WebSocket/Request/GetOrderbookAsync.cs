using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Request
{
    public class GetOrderbookAsync : BaseRequestAsync<GetOrderbookReply>
    {
        public GetOrderbookAsync(string venue, Stock stock)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}", venue, stock.Symbol);
        }
    }
}
