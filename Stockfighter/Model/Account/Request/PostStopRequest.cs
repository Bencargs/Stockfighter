namespace Stockfighter.Model.Account.Request
{
    public class PostStopRequest : BaseRequest
    {
        public PostStopRequest(int instance)
        {
            Url = string.Format("https://www.stockfighter.io/gm/instances/{0}/stop", instance);
        }

        public override bool Execute()
        {
            return base.Execute<BaseReply, PostStopRequest>(this);
        }
    }
}
