using Newtonsoft.Json;

namespace Stockfighter.Model.Account.Reply
{
    public class GetInstanceReply : BaseReply
    {
        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("done")]
        public bool Done { get; set; }

        [JsonProperty("id")]
        public int LevelId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        public GetInstanceReply(bool success, string error = null) : base(success, error)
        {
        }

        public override string ToString()
        {
            return string.Format("Class: {0}\n" +
                                 "{1}, Details: {2}, Done: {3}, Id: {4}, State: {5}",
                                 GetType().Name, base.ToString(), Details, Done, LevelId, State);
        }
    }
}
