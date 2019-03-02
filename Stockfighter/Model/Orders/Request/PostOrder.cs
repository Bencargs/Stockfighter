using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stockfighter.Config;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.Orders.Request
{
    public class PostOrder : BaseRequest
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

        //private readonly Utils.Direction _direction;
        [JsonConverter(typeof (StringEnumConverter))]
        [JsonProperty("direction")]
        public string Direction { get; set; }

        //{
            //get { return Utils.GetDirection(_direction); }
        //}

        private readonly Utils.OrderType _orderType;

        [JsonProperty("orderType")]
        public string OrderType
        {
            get { return Utils.GetOrderType(_orderType); }
        }

        public PostOrder(string account, string venue, Stock stock, Fill order, Utils.OrderType orderType)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/orders", venue, stock.Symbol);
            Account = account;
            Venue = venue;
            Stock = stock.Symbol;
            Price = order.Price;
            Quantity = order.Quantity;
            Direction = order.IsBuy ? "buy" : "sell";
            //_direction = order Utils.GetDirection(order.IsBuy);
            _orderType = orderType;
        }

        public override bool Execute()
        {
            return base.Execute<OrderReply, PostOrder>(this);
        }
    }
}
