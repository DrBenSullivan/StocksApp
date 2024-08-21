using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class BuyOrder : Order
    {
        [Key]
        public Guid BuyOrderID { get; set; } = Guid.NewGuid();
        public override string TradeType => "BuyOrder";
    }
}
