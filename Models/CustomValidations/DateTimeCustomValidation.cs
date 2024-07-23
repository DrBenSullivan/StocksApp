using System.ComponentModel.DataAnnotations;

namespace StocksApplication.Models.CustomValidations
{
	public class DateRangeAttribute : ValidationAttribute
	{
		private readonly DateTime _minDate = new DateTime(2000, 1, 1);
		private readonly DateTime _maxDate = DateTime.Now;

		public DateRangeAttribute() { }

		protected override ValidationResult? IsValid(object? value, ValidationContext context)
		{
			if (value != null || value is not DateTime dateTime) return new ValidationResult("A valid date must be provided.");

			if (dateTime < _minDate || dateTime > _maxDate) return new ValidationResult("A valid date between 01-01-2000 and now must be provided");

			return ValidationResult.Success;
		}
	}

}
