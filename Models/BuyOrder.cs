using System.ComponentModel.DataAnnotations;

namespace StocksAppWithConfiguration.Models
{
	public class BuyOrder
	{
		Guid? BuyOrderID { get; set; } = Guid.NewGuid();

		[Required]
		string StockSymbol { get; set; }

		[Required]
		string StockName { get; set; }

		DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;

		[Range(1, 100000)]
		int? Quantity { get; set; }

		[Range(1, 10000)]
		double? Price { get; set; }
	}
}
