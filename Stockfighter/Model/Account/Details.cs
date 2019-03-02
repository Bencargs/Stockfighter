using Newtonsoft.Json;

namespace Stockfighter.Model.Account
{
    public class Details
    {
        [JsonProperty("endOfTheWorldDay")]
        public int EndDay { get; set; }

        [JsonProperty("tradingDay")]
        public int TradingDay { get; set; }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "End Day: {1}, Trading Day : {2}",
                                 GetType().Name, EndDay, TradingDay);
        }
    }
}
