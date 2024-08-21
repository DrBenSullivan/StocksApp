using System.ComponentModel.DataAnnotations;

namespace StocksApp.Presentation.Models.ViewModels
{
    public class StockTradeViewModel
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; } = 0.00;
        [Range(0, 100000, ErrorMessage = "Quantity must be between 1 and 100,000 units.")]
        public int Quantity { get; set; } = 0;
    }
}
