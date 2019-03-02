using System;
using Stockfighter.Config;
using Stockfighter.Model.Account.Reply;
using Stockfighter.Model.Orders;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Controller.Levels
{
    public class Level1
    {
        public void Execute()
        {
            GameManager manager = new GameManager();
            StartReply level = manager.StartLevel("first_steps");
            int instance = level.InstanceId;

            string account = level.Account;
            string venueName = level.Venues[0];
            string symbol = level.Stocks[0];

            Market market = new Market(account, level.Venues);

            market.MonitorStock(venueName, symbol);

            for (int i = 0; i < 100; i++)
            {
                GetQuoteReply quote = market.GetQuote(venueName, symbol);
                market.PlaceOrder(venueName, symbol, new Fill(quote.AskSize, 1, Utils.Direction.Buy), Utils.OrderType.Limit);
            }

            Console.ReadKey();

            //var values = market.GetQuotes(venueName, symbol).Where(x => x.Ask != 0 && x.Bid != 0);
            //string csv = string.Join(Environment.NewLine, values.Select(d => d.LastTrade.ToString("HH:mm:ss.ffffff") + "," + d.Ask + "," + d.Bid + "," + d.Last));

            manager.StopLevel(instance);
        }
    }
}
