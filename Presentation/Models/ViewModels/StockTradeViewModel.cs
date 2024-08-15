using System.ComponentModel.DataAnnotations;

namespace StocksApp.Presentation.Models.ViewModels
{
    public class StockTradeViewModel
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; }
		[Range(0, 100000, ErrorMessage = "A valid stock Quantity between 1 - 100,000 was not provided.")]
        public int Quantity { get; set; }
    }
}
