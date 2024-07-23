namespace StocksApp.Models.ViewModels
{
    public class StockTradeViewModel
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
