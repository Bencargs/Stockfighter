using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockfighter.Data;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.WebSocket.Reply;
using Stock = Stockfighter.Model.Stock;

namespace Stockfighter.Controller
{
    public class DataAccess
    {
        public void UpdateStocks(List<Stock> stocks)
        {
            //using (var dc = new Trades())
            //{
            //    dc.Database.CreateIfNotExists();
            //    Stock yt = dc.Stocks.Create<Stock>();
            //    yt.Name = "test";
            //    yt.Symbol = "ANC";
            //    dc.Stocks.AddOrUpdate(yt);
            //    dc.SaveChanges();
            //}
        }

        public void UpdateQuote(GetQuoteReply quote)
        {
        }

        public void UpdateOrder(OrderReply order)
        {
            
        }

        public void UpdateOrderbook(GetOrderbookReply orderbook)
        {
            
        }

        public void UpdateExecution(ExecutionTickerReply execution)
        {
            
        }
    }
}
