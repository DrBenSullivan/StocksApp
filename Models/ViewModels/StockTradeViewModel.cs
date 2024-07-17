namespace StocksAppWithConfiguration.Models.ViewModels
{
    public class StockTradeViewModel
    {
        string? StockSymbol { get; set; }
        string? StockName { get; set; }
        double Price { get; set; }
        int Quantity { get; set; }
    }
}
