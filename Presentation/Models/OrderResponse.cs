namespace StocksApp.Presentation.Models
{
	public abstract class OrderResponse
	{
		public virtual string? TradeType { get; set; }
		public string? StockSymbol { get; set; }
		public string? StockName { get; set; }
		public DateTime DateAndTimeOfOrder { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public double TradeAmount { get; set; }
	}
}
