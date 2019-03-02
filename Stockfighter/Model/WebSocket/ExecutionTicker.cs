using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.WebSocket.Reply;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace Stockfighter.Model.WebSocket
{
    public class ExecutionTicker : BaseTicker
    {
        public EventHandler<ExecutionTickerReply> ExecutionUpdate { get; set; }
        public EventHandler<OrderReply> OrderUpdate { get; set; }
        public string Venue { get; private set; }
        public Stock Stock { get; private set; }

        public ExecutionTicker(string account, string venue)
            :base(account)
        {
            Venue = venue;
            MessageReceived += Receive;
            Url = string.Format("wss://api.stockfighter.io/ob/api/ws/{0}/venues/{1}/executions", Account, venue);
        }

        public ExecutionTicker(string account, string venue, Stock stock)
            : this(account, venue)
        {
            Stock = stock;
            Url = string.Format("wss://api.stockfighter.io/ob/api/ws/{0}/venues/{1}/executions/stocks/{2}",
                    Account, Venue, Stock.Symbol);
        }

        private void Receive(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                ExecutionTickerReply reply = JsonConvert.DeserializeObject<ExecutionTickerReply>(e.Message);
                if (reply != null)
                {
                    EventHandler<ExecutionTickerReply> executionHandler = ExecutionUpdate;
                    if (executionHandler != null)
                    {
                        executionHandler(this, reply);
                    }

                    EventHandler<OrderReply> quoteHandler = OrderUpdate;
                    if (quoteHandler != null)
                    {
                        quoteHandler(this, reply.Order);
                    }
                }
            }
            catch (SocketException ex)
            {
                //reconnect    
            }
            catch (Exception ex)
            {
                Error(sender, new ErrorEventArgs(new Exception("Unsupported message received.\nex: " + ex.Message + "\nMessage: " + e.Message)));
            }
        }
    }
}
