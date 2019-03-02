namespace Stockfighter.Model.WebSocket.Request
{
    public class GetHeartbeatAsync : BaseRequestAsync<BaseReply>
    {
        public GetHeartbeatAsync()
        {
            Url = "https://api.stockfighter.io/ob/api/heartbeat";
        }

        public GetHeartbeatAsync(string venue)
            : this()
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/heartbeat", venue);
        }
    }
}
