using Newtonsoft.Json;

namespace Stockfighter.Model
{
    public class Stock
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        public Stock()
        {
        }

        public Stock(string name, string symbol)
            : this()
        {
            Name = name;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Symbol: {1}", Name, Symbol);
        }
    }
}
