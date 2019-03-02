using Stockfighter.Model.Account.Reply;

namespace Stockfighter.Model.Account.Request
{
    public class PostStartRequest : BaseRequest
    {
        public PostStartRequest(string level)
        {
            Url = string.Format("https://www.stockfighter.io/gm/levels/{0}", level);
        }

        public override bool Execute()
        {
            return base.Execute<StartReply, PostStartRequest>(this);
        }
    }
}
