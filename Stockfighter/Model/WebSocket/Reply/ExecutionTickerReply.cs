using System;
using Newtonsoft.Json;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.WebSocket.Reply
{
    public class ExecutionTickerReply : BaseReply
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("symbol")]
        public string Stock { get; set; }

        [JsonProperty("order")]
        public OrderReply Order { get; set; }

        [JsonProperty("standingId")]
        public int StandingId { get; set; }

        [JsonProperty("incomingId")]
        public int IncomingId { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("filled")]
        public int Filled { get; set; }

        [JsonProperty("filledAt")]
        public DateTime FilledAt { get; set; }

        [JsonProperty("standingComplete")]
        public bool StandingComplete { get; set; }

        [JsonProperty("incomingComplete")]
        public bool IncomingComplete { get; set; }

        public ExecutionTickerReply(bool success, string error = null) 
            : base(success, error)
        {
        }

        public override bool Validate()
        {
            return base.Validate() && Order != null;
        }

        public override string ToString()
        {
            return string.Format("Class: ExecutionTickerReply\n" +
                                 "{0}, Account: {1}, Venue: {2}, Symbol: {3}, StandingId: {4}, IncomingId: {5}, " +
                                 "Price: {6}, Filled: {7}, FilledAt: {8}, StandingComplete: {9}, IncomingComplete: {10}\nOrder:\n{11}",
                                base.ToString(), Account, Venue, Stock, StandingId, IncomingId, Price, Filled, FilledAt, 
                                StandingComplete, IncomingComplete, Order);
        }
    }
}
