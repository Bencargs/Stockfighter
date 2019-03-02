namespace Stockfighter.Model.Account.Request
{
    public class PostResumeRequest : BaseRequest
    {
        public PostResumeRequest(int instance)
        {
            Url = string.Format("https://www.stockfighter.io/gm/instances/{0}/resume", instance);
        }

        public override bool Execute()
        {
            return base.Execute<BaseReply, BaseRequest>(this);
        }
    }
}
