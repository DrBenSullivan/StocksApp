using Microsoft.Extensions.Options;

namespace StocksApplication.Models
{
	public class TradingOptions
	{
		public string? DefaultStockSymbol { get; set; }
	}
}
