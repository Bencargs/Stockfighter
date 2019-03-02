using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Request
{
    public class GetOrderAsync : BaseRequestAsync<OrderReply>
    {
        public GetOrderAsync(string venue, Stock stock, int id)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/orders/{2}", venue, stock.Symbol, id);
        }
    }
}
