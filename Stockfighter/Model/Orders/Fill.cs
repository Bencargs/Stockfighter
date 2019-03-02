using Newtonsoft.Json;
using Stockfighter.Config;

namespace Stockfighter.Model.Orders
{
    public class Fill
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("qty")]
        public int Quantity { get; set; }

        [JsonProperty("isBuy")]
        public bool IsBuy { get; set; }

        public Fill(int price, int quantity, Utils.Direction direction)
        {
            Price = price;
            Quantity = quantity;
            IsBuy = direction == Utils.Direction.Buy;
        }

        public override string ToString()
        {
            return string.Format("Price: {0}, Quantity: {1}, {2}", Price, Quantity, IsBuy ? "Buy" : "Sell");
        }
    }
}
