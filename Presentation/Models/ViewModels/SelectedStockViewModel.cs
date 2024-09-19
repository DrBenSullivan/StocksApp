namespace StocksApp.Presentation.Models.ViewModels
{
    public class SelectedStockViewModel
    {
        public string? StockName { get; set; }
        public string? StockSymbol { get; set; }
        public string? LogoUrl { get; set; }
        public string? Sector { get; set; }
        public string? Exchange { get; set; }
        public double? Price { get; set; }
    }
}
