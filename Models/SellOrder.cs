using System.ComponentModel.DataAnnotations;

namespace StocksApplication.Models
{
	public class SellOrder
	{
		Guid? SellOrderID { get; set; } = Guid.NewGuid();

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
