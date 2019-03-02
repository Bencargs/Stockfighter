using Newtonsoft.Json;

namespace Stockfighter.Model
{
    public class BaseReply : IReply
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public string Json { get; set; }

        public bool Success { get; set; }

        public BaseReply(bool success, string error = null)
        {
            Success = success;
            Error = error;
        }
        
        public virtual bool Validate()
        {
            return Success && string.IsNullOrWhiteSpace(Error);
        }
        
        public override string ToString()
        {
            return string.Format("Success: {0}, Error: {1}", Success, Error ?? "N/A");
        }
    }
}
