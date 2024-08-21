namespace StocksApp.Presentation.Models
{
    public class SellOrderResponse : OrderResponse
    {
        public Guid SellOrderID { get; set; } = Guid.NewGuid();
		public override string TradeType => "SellOrder";

		public override bool Equals(object? obj)
		{
            if (obj == null) return false;
            if (obj is not SellOrderResponse other) return false;

            return  SellOrderID == other.SellOrderID &&
                    StockSymbol == other.StockSymbol &&
                    StockName == other.StockName &&
                    DateAndTimeOfOrder == other.DateAndTimeOfOrder &&
                    Quantity == other.Quantity &&
                    Price == other.Price &&
                    TradeAmount == other.TradeAmount;
		}

		public override int GetHashCode()
		{
		    return HashCode.Combine(SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price);
		}
	}
}
