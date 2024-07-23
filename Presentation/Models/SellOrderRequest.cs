using System.ComponentModel.DataAnnotations;

namespace StocksApp.Presentation.Models
{
    public class SellOrderRequest : IValidatableObject
    {
        [Required(ErrorMessage = "A StockSymbol was not provided. StockSymbol cannot be null or empty.")]
		public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "A StockName was not provided. StockName cannot be null or empty.")]
        public string? StockName { get; set; }

        public DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;

        [Range(1, 100_000, ErrorMessage = "A valid stock Quantity between 1 - 100,000 was not provided.")]
        public int Quantity { get; set; }

        [Range(1, 10_000, ErrorMessage = "A valid stock Price between 1 - 10,000 was not provided")]
        public double Price { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (StockSymbol is null) yield return new ValidationResult("A StockSymbol was not provided. StockSymbol cannot be null or empty.");
			if (StockName is null) yield return new ValidationResult("A StockName was not provided. StockName cannot be null or empty.");
			if (DateAndTimeOfOrder < new DateTime(2000, 1, 1)) yield return new ValidationResult("A valid DateAndTimeOfOrder after 31 December 1999 must be provided.");
			if (Quantity < 1 || Quantity > 100_000) yield return new ValidationResult($"A valid Quantity is between 1 - 100,000. Quantity provided: {Quantity}");
			if (Price < 1 || Price > 10_000) yield return new ValidationResult($"A valid Price is between 1 - 10,000. Price provided: {Price}");
		}
	}
}

