using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class SellOrder : Order
    {
        [Key]
        public Guid SellOrderID { get; set; } = Guid.NewGuid();
        [Required]
        public override string TradeType => "SellOrder";
    }
}
