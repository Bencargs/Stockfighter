using Stockfighter.Model.Account.Reply;

namespace Stockfighter.Model.Account.Request
{
    public class GetInstanceRequest : BaseRequest
    {
        public GetInstanceRequest(int instance)
        {
            Url = string.Format("https://www.stockfighter.io/gm/instances/{0}", instance);
        }

        public override bool Execute()
        {
            return base.Execute<GetInstanceReply>().Result;
        }
    }
}
