using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Stockfighter.Model.Orders.Reply
{
    public class GetQuoteReply : BaseReply
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        //http://weblogs.asp.net/manavi/associations-in-ef-4-1-code-first-part-5-one-to-one-foreign-key-associations
        [JsonIgnore]
        public long StockId { get; set; }

        [ForeignKey("StockId"), JsonIgnore]
        public Stock Stock { get; set; }

        [JsonProperty("bid")]
        public int Bid { get; set; }

        [JsonProperty("ask")]
        public int Ask { get; set; }

        [JsonProperty("bidSize")]
        public int BidSize { get; set; }

        [JsonProperty("askSize")]
        public int AskSize { get; set; }

        [JsonProperty("bidDepth")]
        public int BidDepth { get; set; }

        [JsonProperty("askDepth")]
        public int AskDepth { get; set; }

        [JsonProperty("last")]
        public int Last { get; set; }

        [JsonProperty("lastSize")]
        public int LastSize { get; set; }

        [JsonProperty("lastTrade")]
        public DateTime LastTrade { get; set; }

        [JsonProperty("quoteTime")]
        public DateTime Timestamp { get; set; }

        public GetQuoteReply(bool success, string error = null) 
            : base(success, error)
        {
        }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "{1}, Venue: {2}, Symbol: {3}, Bid: {4}, Ask: {5}, Bid Size: {6}, Ask Size: {7}, " +
                                 "Bid Depth: {8}, Ask Depth: {9}, Last: {10}, Last Size: {11}, Last Trade: {12}, Timestamp: {13}",
                                GetType().Name, base.ToString(), Venue, Symbol, Bid, Ask, BidSize, AskSize, BidDepth, AskDepth, 
                                Last, LastSize, LastTrade, Timestamp);
        }

        public override bool Equals(object obj)
        {
            GetQuoteReply o = obj as GetQuoteReply;
            if (o != null)
            {
                return Venue == o.Venue &&
                       Symbol == o.Symbol &&
                       Bid == o.Bid &&
                       Ask == o.Ask &&
                       BidSize == o.BidSize &&
                       AskSize == o.AskSize &&
                       BidDepth == o.BidDepth &&
                       AskDepth == o.AskDepth &&
                       Last == o.Last &&
                       LastSize == o.LastSize &&
                       LastTrade == o.LastTrade &&
                       Last == o.Last &&
                       Timestamp == o.Timestamp;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Venue.GetHashCode() ^ Symbol.GetHashCode() ^ Last;
        }
    }
}
