using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stockfighter.Model;
using Stockfighter.Model.Orders;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.Orders.Request;
using Stockfighter.Model.WebSocket;
using Stockfighter.Model.WebSocket.Reply;
using Stockfighter.Model.WebSocket.Request;
using OrderType = Stockfighter.Config.Utils.OrderType;


namespace Stockfighter.Controller
{
    public class Venue
    {
        public string Name { get; set; }

        public EventHandler<GetStocksReply> StocksUpdated;
        public EventHandler<GetQuoteReply> QuoteUpdated;
        public EventHandler<GetOrderbookReply> OrderbookUpdated;
        public EventHandler<OrderReply> OrderUpdated;
        public EventHandler<ExecutionTickerReply> ExecutionUpdated;
        public EventHandler<ErrorEventArgs> Error;

        private BaseReply _heartbeat;
        public BaseReply Heartbeat
        {
            get { return _heartbeat ?? (_heartbeat = GetHeartbeat()); }
        }

        private readonly Dictionary<string, Stock> _stockLookup;
        private GetStocksReply _stocks;
        public GetStocksReply Stocks
        {
            get { return _stocks ?? (_stocks = GetStocks()); }
        }

        private readonly DataAccess _dataAccess;
        private readonly Dictionary<Stock, ExecutionTicker> _executionTicker;
        private readonly Dictionary<Stock, StockTicker> _stockTicker;

        public Venue(string name)
        {
            Name = name;
            _dataAccess = new DataAccess();
            _stockLookup = new Dictionary<string, Stock>();
            _stockTicker = new Dictionary<Stock, StockTicker>();
            _executionTicker = new Dictionary<Stock, ExecutionTicker>();
            //StocksUpdated += (sender, reply) => UpdateStocks(reply);
            //QuoteUpdated += (sender, reply) => UpdateQuote(reply);
            //OrderbookUpdated += (sender, reply) => UpdateOrderbook(reply);
            //OrderUpdated += (sender, reply) => UpdateOrder(reply);
            //ExecutionUpdated += (sender, reply) => UpdateExecution(reply);
        }

        public Stock GetStock(string symbol)
        {
            Stock stock;
            if (!_stockLookup.TryGetValue(symbol, out stock))
            {
                stock = Stocks.Symbols.FirstOrDefault(s => s.Symbol == symbol);
                GetQuoteAsync(stock);
            }
            return stock;
        }

        public GetStocksReply GetStocks()
        {
            GetStocks request = new GetStocks(Name);
            bool success = request.Execute();
            GetStocksReply reply = (request.Reply as GetStocksReply) ?? new GetStocksReply(false);
            if (success)
            {
                UpdateStocks(reply);
            }
            return reply;
        }

        public BaseReply GetHeartbeat()
        {
            GetHeartbeat request = new GetHeartbeat(Name);
            request.Execute();
            BaseReply reply = request.Reply as BaseReply;
            return reply ?? new BaseReply(false);
        }

        public GetQuoteReply GetQuote(Stock stock)
        {
            GetQuote request = new GetQuote(Name, stock);
            bool success = request.Execute();
            GetQuoteReply reply = (request.Reply as GetQuoteReply) ?? new GetQuoteReply(false);
            if (success)
            {
                UpdateQuote(reply);
                return reply;
            }
            return null;
        }

        public OrderReply GetOrder(Stock stock, int id)
        {
            GetOrder request = new GetOrder(Name, stock, id);
            bool success = request.Execute();
            OrderReply reply = (request.Reply as OrderReply) ?? new OrderReply(false);
            if (success)
            {
                UpdateOrder(reply);
            }
            return reply;
        }

        public OrderReply PostOrder(string account, Stock stock, Fill order, OrderType orderType)
        {
            PostOrder request = new PostOrder(account, Name, stock, order, orderType);
            bool success = request.Execute();
            OrderReply reply = (request.Reply as OrderReply) ?? new OrderReply(false);
            if (success)
            {
                UpdateOrder(reply);
            }
            return reply;
        }

        public OrderReply CancelOrder(Stock stock, int id)
        {
            CancelOrder request = new CancelOrder(Name, stock, id);
            bool success = request.Execute();
            OrderReply reply = (request.Reply as OrderReply) ?? new OrderReply(false);
            if (success)
            {
                UpdateOrder(reply);
            }
            return reply;
        }

        public GetOrderbookReply GetOrderbook(Stock stock)
        {
            GetOrderbook request = new GetOrderbook(Name, stock);
            bool success = request.Execute();
            GetOrderbookReply reply = (request.Reply as GetOrderbookReply) ?? new GetOrderbookReply(false);
            if (success)
            {
                UpdateOrderbook(reply);
            }
            return reply;
        }

        public void GetStocksAsync()
        {
            GetStocksAsync request = new GetStocksAsync(Name);
            request.ExecuteComplete += StocksUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void GetQuoteAsync(Stock stock)
        {
            GetQuoteAsync request = new GetQuoteAsync(Name, stock);
            request.ExecuteComplete += QuoteUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void GetOrderAsync(Stock stock, int id)
        {
            GetOrderAsync request = new GetOrderAsync(Name, stock, id);
            request.ExecuteComplete += OrderUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void PostOrderAsync(string account, Stock stock, Fill order, OrderType orderType)
        {
            PostOrderAsync request = new PostOrderAsync(account, Name, stock, order, orderType);
            request.ExecuteComplete += OrderUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void CancelOrderAsync(Stock stock, int id)
        {
            CancelOrderAsync request = new CancelOrderAsync(Name, stock, id);
            request.ExecuteComplete += OrderUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void GetOrderbookAsync(Stock stock)
        {
            GetOrderbookAsync request = new GetOrderbookAsync(Name, stock);
            request.ExecuteComplete += OrderbookUpdated;
            request.Error += Error;
            request.Execute();
        }

        public void SubscribeOrder(string account, Stock stock)
        {
            ExecutionTicker ticker;
            if (!_executionTicker.TryGetValue(stock, out ticker))
            {
                ticker = new ExecutionTicker(account, Name, stock);
                ticker.ExecutionUpdate += ExecutionUpdated;
                ticker.OrderUpdate += OrderUpdated;
                ticker.Execute();
                _executionTicker.Add(stock, ticker);
            }
        }

        public void SubscribeQuote(string account, Stock stock)
        {
            StockTicker ticker;
            if (!_stockTicker.TryGetValue(stock, out ticker))
            {
                ticker = new StockTicker(account, Name, stock);
                ticker.QuoteUpdate += QuoteUpdated;
                ticker.Execute();
                _stockTicker.Add(stock, ticker);
            }
        }

        public void SubscribeVenue(string account)
        {
            Stock stock = new Stock();
            ExecutionTicker executionTicker;
            if (!_executionTicker.TryGetValue(stock, out executionTicker))
            {
                executionTicker = new ExecutionTicker(account, Name);
                executionTicker.ExecutionUpdate += ExecutionUpdated;
                executionTicker.OrderUpdate += OrderUpdated;
                executionTicker.Execute();
            }

            StockTicker stockTicker;
            if (!_stockTicker.TryGetValue(stock, out stockTicker))
            {
                stockTicker = new StockTicker(account, Name);
                stockTicker.QuoteUpdate += QuoteUpdated;
                stockTicker.Execute();
            }
        }

        private void UpdateStocks(GetStocksReply reply)
        {
            foreach (Stock stock in reply.Symbols)
            {
                _stockLookup[stock.Symbol] = stock;
            }

            //EventHandler<GetStocksReply> handler = StocksUpdated;
            //if (handler != null)
            //{
            //    handler(null, reply);
            //}
            _dataAccess.UpdateStocks(reply.Symbols);
        }

        private void UpdateQuote(GetQuoteReply reply)
        {
            //EventHandler<GetQuoteReply> handler = QuoteUpdated;
            //if (handler != null)
            //{
            //    handler(null, reply);
            //}
            _dataAccess.UpdateQuote(reply);
        }

        private void UpdateOrder(OrderReply reply)
        {
            //EventHandler<OrderReply> handler = OrderUpdated;
            //if (handler != null)
            //{
            //    handler(null, reply);
            //}
            _dataAccess.UpdateOrder(reply);
        }

        private void UpdateOrderbook(GetOrderbookReply reply)
        {
            //EventHandler<GetOrderbookReply> handler = OrderbookUpdated;
            //if (handler != null)
            //{
            //    handler(null, reply);
            //}
            _dataAccess.UpdateOrderbook(reply);
        }

        private void UpdateExecution(ExecutionTickerReply reply)
        {
            EventHandler<ExecutionTickerReply> handler = ExecutionUpdated;
            if (handler != null)
            {
                handler(null, reply);
            }
            _dataAccess.UpdateExecution(reply);
        }
    }
}
