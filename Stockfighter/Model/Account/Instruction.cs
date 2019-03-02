using Newtonsoft.Json;

namespace Stockfighter.Model.Account
{
    public class Instruction
    {
        [JsonProperty("Instructions")]
        public string Instructions { get; set; }

        [JsonProperty("Order Types")]
        public string OrderTypes { get; set; }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "Instructions: {1}, Order Types: {2}",
                                 GetType().Name, Instructions, OrderTypes);
        }
    }
}
