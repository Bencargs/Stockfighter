using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stockfighter.Model.Account.Reply
{
    public class StartReply : BaseReply
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("instanceId")]
        public int InstanceId { get; set; }

        [JsonProperty("instructions")]
        public Instruction Instructions { get; set; }

        [JsonProperty("secondsPerTradingDay")]
        public int SecondsPerDay { get; set; }

        [JsonProperty("tickers")]
        public List<string> Stocks { get; set; }

        [JsonProperty("venues")]
        public List<string> Venues { get; set; }

        [JsonProperty("balances")]
        public Balance Balances { get; set; }

        public StartReply(bool success, string error = null) : base(success, error)
        {
        }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "{1}, Account: {2}, InstanceId: {3}, Instructions: {4}, SecondsPerDay: {5}, " +
                                 "Symbols: {6}, Venues: {7}, Balances: {8}",
                                 GetType().Name, base.ToString(), Account, InstanceId, Instructions, SecondsPerDay,
                                 Stocks != null ? string.Join("\n", Stocks) : "",
                                 Venues != null ? string.Join("\n", Venues) : "",
                                 Balances);
        }
    }
}
