using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stockfighter.Model.Orders.Reply
{
    public class GetOrderbookReply : BaseReply
    {
        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("bids")]
        public List<Fill> Bids { get; set; }

        [JsonProperty("asks")]
        public List<Fill> Asks { get; set; }

        public GetOrderbookReply(bool success, string error = null) 
            : base(success, error)
        {
        }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "{1}, Venue: {2}, Symbol: {3}, Timestamp: {4} \nBids:\n{5} \nAsks:\n{6}", 
                                GetType().Name, base.ToString(), Venue, Symbol, Timestamp, 
                                Bids != null ? string.Join("\n", Bids) : "",
                                Asks != null ? string.Join("\n", Asks) : "");
        }
    }
}
