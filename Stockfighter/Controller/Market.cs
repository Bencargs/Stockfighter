using System;
using System.Collections.Generic;
using System.Linq;
using Stockfighter.Model;
using Stockfighter.Model.Orders;
using Stockfighter.Model.Orders.Reply;
using Stockfighter.Model.WebSocket.Reply;
using OrderType = Stockfighter.Config.Utils.OrderType;

namespace Stockfighter.Controller
{
    public class Market
    {
        public Dictionary<Stock, Dictionary<int, OrderReply>> Orders;
        public Dictionary<Stock, SortedList<DateTime, GetOrderbookReply>> Orderbooks;
        public Dictionary<Stock, SortedList<DateTime, GetQuoteReply>> Quotes;
        private readonly Dictionary<string, Venue> _venues;
        private readonly string _account;

        public Market(string account, List<string> venues)
        {
            _account = account;
            Orders = new Dictionary<Stock, Dictionary<int, OrderReply>>();
            Orderbooks = new Dictionary<Stock, SortedList<DateTime, GetOrderbookReply>>();
            Quotes = new Dictionary<Stock, SortedList<DateTime, GetQuoteReply>>();
            _venues = new Dictionary<string, Venue>();
            foreach (string v in venues)
            {
                Venue venue = new Venue(v);
                AddVenue(venue);
            }
        }

        public Venue GetVenue(string venueName)
        {
            Venue venue;
            if (_venues.TryGetValue(venueName, out venue))
            {
                return venue;
            }
            return null;
        }

        public Stock GetStock(string venueName, string symbol)
        {
            Venue venue = GetVenue(venueName);
            if (venue != null)
            {
                return venue.GetStock(symbol);
            }
            return null;
        }

        public void MonitorStock(string venueName, string stockName)
        {
            Venue venue = new Venue(venueName);
            venue.QuoteUpdated += (sender, reply) => UpdateQuote(reply);
            venue.OrderUpdated += (sender, reply) => UpdateOrder(reply);
            venue.OrderbookUpdated += (sender, reply) => UpdateOrderbook(reply);
            venue.ExecutionUpdated += ExecutionUpdated;
            
            Stock stock = venue.GetStock(stockName);
            venue.SubscribeQuote(_account, stock);
            venue.SubscribeOrder(_account, stock);
        }

        public void AddVenue(Venue venue)
        {
            if (!_venues.ContainsKey(venue.Name))
            {
                _venues.Add(venue.Name, venue);
                _venues[venue.Name].OrderUpdated += (sender, reply) => UpdateOrder(reply);
                _venues[venue.Name].QuoteUpdated += (sender, reply) => UpdateQuote(reply);
                //_venues[venue.Name].OrderbookUpdated += (sender, reply) => UpdateOrderbook(reply);
                //_venues[venue.Name].ExecutionUpdated += ExecutionUpdated;
            }
        }

        public void PlaceOrder(string venueName, string stockName, Fill order, OrderType orderType)
        {
            Venue venue = GetVenue(venueName);
            if (venue != null)
            {
                //venue.OrderUpdated += (sender, reply) => UpdateOrder(reply);
                //venue.SubscribeOrder(_account, stock);

                Stock stock = GetStock(venueName, stockName);
                if (stock != null)
                {
                    venue.PostOrderAsync(_account, stock, order, orderType);
                    //venue.PostOrder(_account, stock, order, orderType);
                }
            }
        }

        public GetQuoteReply GetQuote(string venueName, string stockName)
        {
            Venue venue = GetVenue(venueName);
            if (venue != null)
            {
                Stock stock = venue.GetStock(stockName);
                if (stock != null)
                {
                    SortedList<DateTime, GetQuoteReply> quote;
                    if (!Quotes.ContainsKey(stock))
                    {
                        GetQuoteReply reply = venue.GetQuote(stock);
                        quote = new SortedList<DateTime, GetQuoteReply> {{reply.LastTrade, reply}};
                        Quotes.Add(stock, quote);
                        return reply;
                    }
                    
                    if (Quotes.TryGetValue(stock, out quote))
                    {
                        return quote.Last().Value;
                    }
                }
            }
            return null;
        }

        public List<GetQuoteReply> GetQuotes(string venueName, string symbol)
        {
            Stock stock = GetStock(venueName, symbol);
            if (stock != null)
            {
                SortedList<DateTime, GetQuoteReply> quotes;
                if (!Quotes.ContainsKey(stock))
                {
                    GetQuoteReply reply = GetQuote(venueName, symbol);
                    return new List<GetQuoteReply> {reply};
                }

                if (Quotes.TryGetValue(stock, out quotes))
                {
                    return quotes.Select(q => q.Value).ToList();
                }
            }
            return null;
        }

        private void ExecutionUpdated(object sender, ExecutionTickerReply executionTickerReply)
        {
            UpdateOrder(executionTickerReply.Order);

            // Logic here to determine compeditors orders
        }

        private void UpdateOrderbook(GetOrderbookReply getOrderbookReply)
        {
            Stock stock = GetStock(getOrderbookReply.Venue, getOrderbookReply.Symbol);

            SortedList<DateTime, GetOrderbookReply> stockOrderbook;
            if (!Orderbooks.TryGetValue(stock, out stockOrderbook))
            {
                stockOrderbook = new SortedList<DateTime, GetOrderbookReply>
                {
                    {getOrderbookReply.Timestamp, getOrderbookReply}
                };
                Orderbooks.Add(stock, stockOrderbook);
            }
            else
            {
                if (!stockOrderbook.ContainsKey(getOrderbookReply.Timestamp))
                {
                    Orderbooks[stock].Add(getOrderbookReply.Timestamp, getOrderbookReply);
                }
            }
        }

        private void UpdateOrder(OrderReply order)
        {
            Stock stock = GetStock(order.Venue, order.Symbol);

            Dictionary<int, OrderReply> stockOrders;
            if (!Orders.TryGetValue(stock, out stockOrders))
            {
                stockOrders = new Dictionary<int, OrderReply>
                {
                    {order.OrderId, order}
                };
                // Add a orderbook for that stock
                Orders.Add(stock, stockOrders);
            }
            else
            {
                OrderReply existingOrder;
                if (!stockOrders.TryGetValue(order.OrderId, out existingOrder))
                {
                    // Add the order if the orderId doesn't exist for that stock
                    Orders[stock].Add(order.OrderId, order);
                }
                else
                {
                    if (existingOrder.Timestamp < order.Timestamp)
                    {
                        // Update order if the reply is newer than current
                        Orders[stock][order.OrderId] = order;
                    }
                }
            }
        }

        private void UpdateQuote(GetQuoteReply getQuoteReply)
        {
            Stock stock = GetStock(getQuoteReply.Venue, getQuoteReply.Symbol);
            // Use last trade rather than quote time
            DateTime timestamp = getQuoteReply.LastTrade;

            SortedList<DateTime, GetQuoteReply> stockQuotes;
            if (!Quotes.TryGetValue(stock, out stockQuotes))
            {
                stockQuotes = new SortedList<DateTime, GetQuoteReply>
                {
                    {timestamp, getQuoteReply}
                };
                Quotes.Add(stock, stockQuotes);
            }
            else
            {
                if (!stockQuotes.ContainsKey(timestamp))
                {
                    Quotes[stock].Add(timestamp, getQuoteReply);
                }
            }
        }
    }
}
