using System.ComponentModel.DataAnnotations;

namespace StocksAppWithConfiguration.Models
{
	public class BuyOrder
	{
		Guid? BuyOrderID { get; set; } = Guid.NewGuid();

		[Required]
		public string StockSymbol { get; set; }

		[Required]
		public string StockName { get; set; }

		public DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;

		[Range(1, 100000)]
		public int? Quantity { get; set; }

		[Range(1, 10000)]
		public double? Price { get; set; }
	}
}
