using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class SellOrder
    {
        public Guid? SellOrderID { get; set; } = Guid.NewGuid();
        public string StockSymbol { get; set; }
        public string StockName { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
