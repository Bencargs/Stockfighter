namespace Stockfighter.Model.Orders.Request
{
    public class GetHeartbeat : BaseRequest
    {
        public GetHeartbeat()
        {
            Url = "https://api.stockfighter.io/ob/api/heartbeat";
        }

        public GetHeartbeat(string venue)
            : this()
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/heartbeat", venue);
        }

        public override bool Execute()
        {
            return Execute<BaseReply>().Result;
        }
    }
}
