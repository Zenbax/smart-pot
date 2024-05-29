using System.ComponentModel.DataAnnotations;

namespace Cloud.Services;

public class ZeroOrOneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && (intValue == 0 || intValue == 1))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("The Enable field must be either 0 or 1.");
    }
}