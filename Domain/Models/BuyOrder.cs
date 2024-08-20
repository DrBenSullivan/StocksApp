using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderID { get; set; } = Guid.NewGuid();
        [StringLength(5)]
        public string? StockSymbol { get; set; }
        [StringLength(100)]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }
    }
}
