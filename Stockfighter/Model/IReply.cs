using Newtonsoft.Json;

namespace Stockfighter.Model
{
    public interface IReply
    {
        [JsonProperty("ok")]
        bool Success { get; set; }

        [JsonIgnore]
        string Json { get; set; }

        bool Validate();
    }
}
