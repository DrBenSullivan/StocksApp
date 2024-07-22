using System.ComponentModel.DataAnnotations;
using StocksAppWithConfiguration.Models.CustomValidations;

namespace StocksAppWithConfiguration.Models.DTOs
{
	public class BuyOrderRequest
	{
		[Required]
		string? StockSymbol { get; set; }

		[Required]
		string? StockName { get; set; }

		[DateRange]
		DateTime DateAndTimeOfOrder { get; set; }
		
		[Range(1, 100000)]
		int Quantity { get; set; }

		[Range(1,10000)]
		double Price { get; set; }
	}
}
