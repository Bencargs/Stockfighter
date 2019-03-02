using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Model.Orders.Request
{
    public class CancelOrder : BaseRequest
    {
        public CancelOrder(string venue, Stock stock, int id)
        {
            Url = string.Format("https://api.stockfighter.io/ob/api/venues/{0}/stocks/{1}/orders/{2}/cancel", venue, stock.Symbol, id);
        }

        public override bool Execute()
        {
            return base.Execute<OrderReply, CancelOrder>(this);
        }
    }
}
