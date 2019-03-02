using System;
using Newtonsoft.Json;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.WebSocket.Reply;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Stockfighter.Model.WebSocket
{
    public class StockTicker : BaseTicker
    {
        public EventHandler<GetQuoteReply> QuoteUpdate { get; set; }
        public Stock Stock { get; private set; }
        public string Venue { get; private set; }

        public StockTicker(string account, string venue)
            : base(account)
        {
            Venue = venue;
            MessageReceived += Recieve;
            Url = string.Format("wss://api.stockfighter.io/ob/api/ws/{0}/venues/{1}/tickertape", Account, venue);
        }

        public StockTicker(string account, string venue, Stock stock)
            : this (account, venue)
        {
            Stock = stock;
            Url = string.Format("wss://api.stockfighter.io/ob/api/ws/{0}/venues/{1}/tickertape/stocks/{2}", Account, Venue, Stock.Symbol);
        }

        private void Recieve(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                StockTickerReply reply = JsonConvert.DeserializeObject<StockTickerReply>(e.Message);
                if (reply != null)
                {
                    EventHandler<GetQuoteReply> handler = QuoteUpdate;
                    if (handler != null)
                    {
                        handler(this, reply.Quote);
                    }
                }
            }
            catch (Exception ex)
            {
                Error(sender, new ErrorEventArgs( new Exception("Unsupported message received.\nex: " + ex.Message + "\nMessage: " + e.Message)));
            }
        }
    }
}
