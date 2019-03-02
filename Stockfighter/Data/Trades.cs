using Stockfighter.Controller;
using Stockfighter.Model;
using Stockfighter.Model.Orders.Reply;

namespace Stockfighter.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Trades : DbContext
    {
        public Trades(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<GetQuoteReply> Quotes { get; set; }
    }
}

//using System;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Data.SQLite;
//using Stockfighter.Model;
//using Stockfighter.Model.Orders.Reply;

//namespace Stockfighter.Data
//{
//    public class Trades : DbContext
//    {
//        private readonly string _dbPath;

//        public Trades(string path)
//            : base(new SQLiteConnection
//            {
//                ConnectionString = new SQLiteConnectionStringBuilder
//                {
//                    DataSource = path,
//                    ForeignKeys = true,
//                    BinaryGUID = false,
//                }.ConnectionString
//            }, true)
//        {
//            _dbPath = path;
//            Database.Log = Console.Write;
//        }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//            Database.SetInitializer(new SQLInitialiser<Trades>(_dbPath, modelBuilder));
//        }

//        public virtual DbSet<Stock> Stocks { get; set; }
//        public virtual DbSet<GetQuoteReply> Quotes { get; set; }
//    }
//}
