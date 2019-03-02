using Newtonsoft.Json;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Reply
{
    public class StockTickerReply : BaseReply
    {
        [JsonProperty("quote")]
        public GetQuoteReply Quote { get; set; }

        public StockTickerReply(bool success, string error = null)
            : base(success, error)
        {
        }

        public override bool Validate()
        {
            return base.Validate() && Quote != null;
        }

        public override string ToString()
        {
            return string.Format("Class: StockTickerReply\n{0}, Quote: {1}", base.ToString(), Quote);
        }
    }
}
