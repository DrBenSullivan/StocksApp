using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class BuyOrder : Order
    {
        [Key]
        public Guid BuyOrderID { get; set; } = Guid.NewGuid();
        [Required]
        public override string TradeType => "BuyOrder";
    }
}
