using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stockfighter.Model.Orders.Reply
{
    public class GetStocksReply : BaseReply
    {
        [JsonProperty("symbols")]
        public List<Stock> Symbols { get; set; }

        public GetStocksReply(bool success, string error = null) 
            : base(success, error)
        {
        }

        public override bool Validate()
        {
            return base.Validate() && Symbols != null;
        }

        public override string ToString()
        {
            return string.Format("{0}\nSymbols:\n{1}", base.ToString(), Symbols != null ? string.Join("\n", Symbols) : "");
        }
    }
}
