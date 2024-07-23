namespace StocksApplication.Models.DTOs
{
	public class SellOrderResponse
	{
		public Guid SellOrderID { get; set; } = Guid.NewGuid();
		
		public string? StockSymbol { get; set; }
		
		public string? StockName { get; set; }
		
		public DateTime DateAndTimeOfOrder { get; set; }
		
		public uint Quantity { get; set; }
		
		public double Price { get; set; }
		
		public double TradeAmount { get; set; }
	}
}
