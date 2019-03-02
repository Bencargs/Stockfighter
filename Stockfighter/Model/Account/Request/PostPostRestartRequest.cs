namespace Stockfighter.Model.Account.Request
{
    public class PostPostRestartRequest : BaseRequest
    {
        public PostPostRestartRequest(int instance)
        {
            Url = string.Format("https://www.stockfighter.io/gm/instances/{0}/restart", instance);
        }

        public override bool Execute()
        {
            return base.Execute<BaseReply, PostPostRestartRequest>(this);
        }
    }
}
