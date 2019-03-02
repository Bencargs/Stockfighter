using System;
using System.CodeDom;
using System.Collections.Generic;
using Stockfighter.Config;
using Stockfighter.Model;
using Stockfighter.Model.Orders.Reply;
using OrderType = Stockfighter.Config.Utils.OrderType;
using Direction = Stockfighter.Config.Utils.Direction;


namespace Stockfighter.Controller
{
    public class Analyser
    {
        private class MonitoringParameter
        {
            public Dictionary<int, int> VolumeHistogram;
            public Dictionary<int, int> PriceMovementHistogram;
            public Dictionary<OrderType, int> OrderTypeCount;

            public int LastPrice;
            public int MaxPrice;
            public int MinPrice;
            public int TotalPrice;
            public double AveragePrice;
            public double PriceVariance;

            public int LastVolume;
            public int MaxVolume;
            public int MinVolume;
            public int TotalVolume;
            public double AverageVolume;
            public double VolumeVariance;

            public DateTime LastTrade;
            public TimeSpan LastTradeDifferance;
            public TimeSpan MaxTradeDifferance;
            public TimeSpan MinTradeDifferance;
            public DateTime TotalTime;
            public TimeSpan AverageTime;
            public TimeSpan TimeVariance;

            public int Spread;
            public int MaxSpread;
            public int MinSpread;
            public int TotalSpread;
            public double AverageSpread;
            public double SpreadVariance;

            public int Run;
            public int MaxRun;
            public int TotalRuns; //> 2
            public double AverageRun;
            public double RunVariance;

            public int TotalTrades;
            public int TotalCancellations;
            public double AverageCancellations;
            public double CancellationVariance;

            public void Update(OrderReply args)
            {
                
            }

            public void Update(GetQuoteReply args)
            {
                
            }

            public void Update(GetOrderbookReply args)
            {
                
            }
        }

        private readonly Dictionary<Stock, Dictionary<Direction, MonitoringParameter>> _monitoringParameters;
        private readonly Venue _venue;

        public Analyser(Venue venue)
        {
            _monitoringParameters = new Dictionary<Stock, Dictionary<Direction, MonitoringParameter>>();
            _venue = venue;

            _venue.QuoteUpdated += (sender, reply) => UpdateMonitoringParameters(reply);
            _venue.OrderbookUpdated += (sender, reply) => UpdateMonitoringParameters(reply);

            _venue.OrderUpdated += (sender, reply) => UpdateMonitoringParameters(reply);
            _venue.ExecutionUpdated += (sender, reply) => UpdateMonitoringParameters(reply.Order);
        }

        private void UpdateMonitoringParameters(GetQuoteReply quote)
        {
            Stock stock = _venue.GetStock(quote.Symbol);

            MonitoringParameter parameter;
            Dictionary<Direction, MonitoringParameter> parameterSides;
            if (!_monitoringParameters.TryGetValue(stock, out parameterSides))
            {
                parameter = new MonitoringParameter();
                parameter.Update(quote);
                parameterSides = new Dictionary<Direction, MonitoringParameter>
                {
                    {Direction.Buy, parameter},
                    {Direction.Sell, parameter}
                };
                _monitoringParameters.Add(stock, parameterSides);
            }
            else
            {
                Direction direction = Direction.Buy;
                if (!parameterSides.TryGetValue(direction, out parameter))
                {
                    parameter = new MonitoringParameter();
                    parameter.Update(quote);
                    parameterSides.Add(direction, parameter);
                }
                else
                {
                    parameter.Update(quote);
                    _monitoringParameters[stock][direction] = parameter;
                }

                direction = Direction.Sell;
                if (!parameterSides.TryGetValue(direction, out parameter))
                {
                    parameter = new MonitoringParameter();
                    parameter.Update(quote);
                    parameterSides.Add(direction, parameter);
                }
                else
                {
                    parameter.Update(quote);
                    _monitoringParameters[stock][direction] = parameter;
                }
            }
        }

        private void UpdateMonitoringParameters(GetOrderbookReply reply)
        {
            
        }

        private void UpdateMonitoringParameters(OrderReply order)
        {
            Stock stock = _venue.GetStock(order.Symbol);
            Direction direction = Utils.GetDirection(order.Direction);

            MonitoringParameter parameter;
            Dictionary<Direction, MonitoringParameter> parameterSides;
            if (!_monitoringParameters.TryGetValue(stock, out parameterSides))
            {
                parameter = new MonitoringParameter();
                parameter.Update(order);
                parameterSides = new Dictionary<Direction, MonitoringParameter>
                {
                    {direction, parameter}
                };
                _monitoringParameters.Add(stock, parameterSides);
            }
            else
            {
                if (!parameterSides.TryGetValue(direction, out parameter))
                {
                    parameter = new MonitoringParameter();
                    parameter.Update(order);
                    parameterSides.Add(direction, parameter);
                }
                else
                {
                    parameter.Update(order);
                    _monitoringParameters[stock][direction] = parameter;
                }
            }
        }
    }
}
