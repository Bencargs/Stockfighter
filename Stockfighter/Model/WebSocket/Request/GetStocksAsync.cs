using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Request
{
    public class GetStocksAsync : BaseRequestAsync<GetStocksReply>
    {
        public GetStocksAsync(string venue)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks", venue);
        }
    }
}
