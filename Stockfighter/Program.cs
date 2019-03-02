using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Stockfighter.Config;
using Stockfighter.Controller;
using Stockfighter.Controller.Levels;
using Stockfighter.Data;
using Stockfighter.Model;
using Stockfighter.Model.Account.Reply;
using Stockfighter.Model.Account.Request;
using Stockfighter.Model.WebSocket.Request;

namespace Stockfighter
{
    class Program
    {
        static void Main(string[] args)
        {
            Level1 game = new Level1();
            game.Execute();
        }

        public void TestingStuff()
        {
            //var db = new SQLInitialiser<Trades>(@"C:\Users\bcargill\Documents\Visual Studio 2012\Projects\Stockfighter\Stockfighter\Stockfighter\Data\Trades.sqlite", new DbModelBuilder(DbModelBuilderVersion.Latest));


            //var db = new SQLiteConnection(@"DataSource=C:\Users\bcargill\Documents\Visual Studio 2012\Projects\Stockfighter\Stockfighter\Stockfighter\Data\Trades.sqlite; Version=3");
            //db.Open();
            //db.Database..cre

            //var db = new Trades(@"DataSource=Trades.sqlite; Version=3");
            ////db.CreateTable<Stock>();

            ////db.Stocks.Create<Stock>();
            //db.Stocks.Create();
            //db.Stocks.Add(new Stock("", "ABC"));
            //db.SaveChanges();

            //GetLevelsRequest levels = new GetLevelsRequest();
            //levels.Execute();
            //Console.WriteLine(levels.Reply);

            //StartRequest start = new StartRequest("first_steps");
            //start.Execute();
            //Console.WriteLine(start.Reply);

            //int instance = (start.Reply as StartReply).InstanceId;
            //Console.WriteLine("------");

            //RestartRequest restart = new RestartRequest(instance);
            //restart.Execute();
            //Console.WriteLine(restart.Reply);

            //Console.WriteLine("------");
            //ResumeRequest resume = new ResumeRequest(instance);
            //resume.Execute();
            //Console.WriteLine(resume.Reply);

            //Console.WriteLine("------");
            //GetInstanceRequest details = new GetInstanceRequest(instance);
            //details.Execute();
            //Console.WriteLine(details.Reply);

            //Console.WriteLine("------");
            //StopRequest stop = new StopRequest(instance);
            //stop.Execute();
            //Console.WriteLine(stop.Reply);


            //var r1 = request1.Reply as StartReply;
            //StopRequest request2 = new StopRequest(r1.InstanceId);
            //request2.Execute();
            //Console.WriteLine(request2.Reply);



            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[Utils.TradesDatabaseKey];
            //if (settings != null)
            //{
            //    // Missing Connection String
            //    throw new Exception();
            //}
            //string fixedConnectionString = settings.ConnectionString.Replace("{AppDir}",
            //    AppDomain.CurrentDomain.BaseDirectory);
            //string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string path = Path.GetDirectoryName(executable);
            //AppDomain.CurrentDomain.SetData("DataDirectory", path);

            //var db = new DataAccess();
            //db.UpdateStocks(new List<Stock>{new Stock("test", "tes")});

            //Venue venue = new Venue("TESTEX");
            //venue.GetHeartbeat();
            //Console.WriteLine(venue.Heartbeat);
            //venue.GetStocks();
            //Console.WriteLine(string.Join("\n", venue.Stocks));

            //GetOrderbook orderbook = new GetOrderbook("TESTEX", "FOOBAR");
            //orderbook.Execute();
            //Console.WriteLine(orderbook.Reply);

            //PostOrder order = new PostOrder("EXB123456", "TESTEX", new Stock("", "FOOBAR"), new Order(0, 100, Direction.Buy), OrderType.Limit);
            //order.Execute();
            //Console.Write(order.Reply);

            //Venue venue = new Venue("TESTEX");
            //var stock = venue.GetStock("FOOBAR");

            //GetQuote request = new GetQuote("TESTEX", "FOOBAR");
            //request.Execute();
            //Console.WriteLine(request.Reply);

            //Stock stock = new Stock("", "FOOBAR");
            //Order order = new Order(100000, 100000, Direction.Buy);
            //PostOrder post = new PostOrder("EXB123456", "TESTEX", stock, order, OrderType.Market);
            //post.Execute();
            //var id = ((OrderReply)post.Reply).Id;

            //CancelOrder cancel = new CancelOrder("TESTEX", stock, id);
            //cancel.Execute();
            //OrderReply reply = cancel.Reply as OrderReply;
            //Console.WriteLine(reply);

            //GetOrder request = new GetOrder("TESTEX", "FOOBAR", id);
            //request.Execute();
            //Console.WriteLine(request.Reply);

            //var test = new StockTicker("EXB123456", "TESTEX", new Stock("", "FOOBAR"));
            //test.QuoteUpdate += (sender, reply) => Console.WriteLine(reply);
            //test.Connect();

            //var test = new ExecutionTicker("EXB123456", "TESTEX");
            //test.ExecutionUpdate += (sender, reply) => Console.WriteLine(reply);
            //Console.WriteLine();
            //test.QuoteUpdate += (sender, reply) => Console.WriteLine(reply);
            //test.Connect();

            //var test = new WebSocket("EXB123456", "TESTEX");
            //test.Error += (sender, eventArgs) => Console.WriteLine(eventArgs.Exception);
            //test.OrderUpdated += (sender, reply) => Console.WriteLine(reply);
            //test.Connect();

            //Stock stock = new Stock("", "FOOBAR");
            //Fill order = new Fill(1, 5, Direction.Buy);
            //PostOrder post = new PostOrder("EXB123456", "TESTEX", stock, order, OrderType.Market);
            //post.Execute();

            //PostOrder post2 = new PostOrder("EXB123456", "TESTEX", stock, new Fill(1, 5, Direction.Sell), OrderType.Market);
            //post2.Execute();

            //var req = new GetOrderbookAsync("TESTEX", new Stock("", "FOOBAR"));
            //req.ExecuteComplete += (sender, reply) => Console.WriteLine(reply);
            //req.Execute();

            //var req = new PostOrderAsync("EXB123456", 
            //                             "TESTEX", 
            //                             new Stock("", "FOOBAR"), 
            //                             new Fill(1, 5, Direction.Buy),
            //                             OrderType.Market);
            //req.ExecuteComplete += (sender, reply) => Console.WriteLine(reply);
            //req.Error += (sender, eventArgs) => Console.WriteLine("Bad Luck Chuck! " + eventArgs);
            //req.Execute();

            //Venue venue = new Venue("TESTEX");
            //venue.StocksUpdated += (sender, reply) => Console.WriteLine(reply);
            //venue.GetStocksAsync();

            //Venue venue = new Venue("TESTEX");
            //venue.ExecutionUpdated += (sender, reply) => Console.WriteLine(reply);
            //venue.Error += (sender, eventArgs) => Console.WriteLine("ERROR: " + eventArgs);
            //venue.SubscribeVenue();
            //venue.SubscribeQuote(new Stock("", "FOOBAR"));

            //Console.ReadKey();
        }
    }
}
