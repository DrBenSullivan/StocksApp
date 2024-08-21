using Microsoft.Extensions.Options;

namespace StocksApp.Domain.Models
{
    public class TradingOptions
    {
        public string? DefaultStockSymbol { get; set; }
    }
}
