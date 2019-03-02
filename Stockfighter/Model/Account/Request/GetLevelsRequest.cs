namespace Stockfighter.Model.Account.Request
{
    public class GetLevelsRequest : BaseRequest
    {
        public GetLevelsRequest()
        {
            Url = string.Format("https://www.stockfighter.io/gm/levels");
        }

        public override bool Execute()
        {
            return base.Execute<BaseReply>().Result;
        }
    }
}
