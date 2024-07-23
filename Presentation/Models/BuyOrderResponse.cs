namespace StocksApp.Presentation.Models
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; } = Guid.NewGuid();

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; } = DateTime.Now;

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }
    }
}
