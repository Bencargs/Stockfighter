using Newtonsoft.Json;

namespace Stockfighter.Model.Account
{
    public class Balance
    {
        [JsonProperty("USD")]
        public int USD { get; set; }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "USD: {1}", GetType().Name, USD);
        }
    }
}
