using System.ComponentModel.DataAnnotations;

namespace StocksApp.Domain.Validators
{
    public class ModelValidationHelper
    {
        public ModelValidationHelper() { }

        internal static void Validate(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (!isValid) throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}
