using Newtonsoft.Json;
using Stockfighter.Config;
using Stockfighter.Model.Orders;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Request
{
    public class PostOrderAsync : BaseRequestAsync<OrderReply>
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("stock")]
        public string Stock { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("qty")]
        public int Quantity { get; set; }

        private readonly Utils.Direction _direction;
        [JsonProperty("direction")]
        public string Direction
        {
            get { return Utils.GetDirection(_direction); }
        }

        private readonly Utils.OrderType _orderType;

        [JsonProperty("orderType")]
        public string OrderType
        {
            get { return Utils.GetOrderType(_orderType); }
        }

        public PostOrderAsync(string account, string venue, Stock stock, Fill order, Utils.OrderType orderType)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/orders", venue, stock.Symbol);
            Account = account;
            Venue = venue;
            Stock = stock.Symbol;
            Price = order.Price;
            Quantity = order.Quantity;
            _direction = Utils.GetDirection(order.IsBuy);
            _orderType = orderType;
        }

        public override void Execute()
        {
            Execute(this);
        }
    }
}
