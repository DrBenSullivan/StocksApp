using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Models
{
    public class SellOrder : Order
    {
        [Key]
        public Guid SellOrderID { get; set; } = Guid.NewGuid();
		public string TradeType = "BuyOrder";
	}
}
