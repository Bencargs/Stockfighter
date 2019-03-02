namespace Stockfighter.Config
{
    public static class Utils
    {
        public static string TradesDatabaseKey = "TradesEntities";
        public static string TestStock = "FOOBAR";
        public static string TestVenue = "TESTEX";
        public static string TestAccount = "EXB123456";
        public static string AuthorisationKey = "X-Starfighter-Authorization";
        public static string ApiKey = "95a51794c5244bed2290352dc8c70596766ad12f";

        public enum Direction
        {
            Buy,
            Sell
        }

        public enum OrderType
        {
            Limit,
            Market,
            FillOrKill,
            ImmediateOrCancell
        }

        public static string GetDirection(Direction direction)
        {
            return direction == Direction.Buy ? "buy" : "sell";
        }

        public static Direction GetDirection(bool isBuy)
        {
            return isBuy ? Direction.Buy : Direction.Sell;
        }

        public static Direction GetDirection(string direction)
        {
            return direction == "buy" ? Direction.Buy : Direction.Sell;
        }

        public static string GetOrderType(OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Market:
                    return "market";
                case OrderType.Limit:
                    return "limit";
                case OrderType.FillOrKill:
                    return "fok";
                case OrderType.ImmediateOrCancell:
                    return "ioc";
            }
            return "";
        }
    }
}
