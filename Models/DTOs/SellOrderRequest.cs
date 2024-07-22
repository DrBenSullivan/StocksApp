using System.ComponentModel.DataAnnotations;
using StocksAppWithConfiguration.Models.CustomValidations;

namespace StocksAppWithConfiguration.Models.DTOs
{
	public class SellOrderRequest
	{
		[Required]
		public string? StockSymbol { get; set; }
		
		[Required]
		public string? StockName { get; set; }
		
		[DateRange]
		public DateTime DateAndTimeOfOrder { get; set; }
		
		[Range(1, 100000)]
		public int Quantity { get; set; }
		
		[Range(1, 10000)]
		public double Price { get; set; }
	}
}

