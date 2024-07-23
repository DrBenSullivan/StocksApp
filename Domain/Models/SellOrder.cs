using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class SellOrder
    {
        Guid? SellOrderID { get; set; } = Guid.NewGuid();
        string StockSymbol { get; set; }
        string StockName { get; set; }
        DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;
        int Quantity { get; set; }
        double Price { get; set; }
    }
}
