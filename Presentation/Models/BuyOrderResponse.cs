using StocksApp.Domain.Models;

namespace StocksApp.Presentation.Models
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; } = Guid.NewGuid();
		public string TradeType = "BuyOrder";

		public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; } = DateTime.Now;

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			if (obj is not BuyOrderResponse other) return false;

			return	BuyOrderID == other.BuyOrderID &&
					StockSymbol == other.StockSymbol &&
					StockName == other.StockName &&
					DateAndTimeOfOrder == other.DateAndTimeOfOrder &&
					Quantity == other.Quantity &&
					Price == other.Price &&
					TradeAmount == other.TradeAmount;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price);
		}
	}
}
