using Microsoft.Extensions.Options;

namespace StocksAppWithConfiguration.Models
{
	public class TradingOptions
	{
		public string? DefaultStockSymbol { get; set; }
	}
}
