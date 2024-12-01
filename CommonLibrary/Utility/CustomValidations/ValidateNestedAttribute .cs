using System.ComponentModel.DataAnnotations;

public class ValidateNestedAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult("Category is required.");
        }

        var context = new ValidationContext(value);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(value, context, results, true))
        {
            return new ValidationResult(string.Join(", ", results.Select(r => r.ErrorMessage)));
        }

        return ValidationResult.Success;
    }
}
