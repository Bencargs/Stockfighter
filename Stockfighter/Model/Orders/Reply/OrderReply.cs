using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stockfighter.Model.Orders.Reply
{
    public class OrderReply : BaseReply
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("originalQty")]
        public int RequestedQuantity { get; set; }

        [JsonProperty("qty")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        [JsonProperty("id")]
        public int OrderId { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("totalFilled")]
        public int TotalFilled { get; set; }

        [JsonProperty("open")]
        public bool Open { get; set; }

        [JsonProperty("fills")]
        public List<Fill> Fills { get; set; }

        public OrderReply(bool success, string error = null)
            : base(success, error)
        {
        }

        public override bool Validate()
        {
            return base.Validate() && Fills != null;
        }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                    "{1}, Symbol: {2}, Venue: {3}, Direction: {4}, RequestedQty: {5}, Quantity: {6}, Price: {7}, OrderType: {8}, " +
                    "Id: {9}, Account: {10}, Timestamp: {11}, TotalFilled: {12}, Open: {13}, \nFills :\n{14}\n", 
                    GetType().Name, base.ToString(), Symbol, Venue, Direction, RequestedQuantity, Quantity, Price, OrderType, 
                    OrderId, Account, Timestamp, TotalFilled, Open, Fills != null ? string.Join("\n", Fills) : "N/A");
        }
    }
}
