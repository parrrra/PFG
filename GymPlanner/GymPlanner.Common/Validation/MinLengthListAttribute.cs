using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Common.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class MinLengthListAttribute : ValidationAttribute
{
    private readonly int _minLength;

    public MinLengthListAttribute(int minLength)
    {
        _minLength = minLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is ICollection list)
        {
            if (list.Count >= _minLength)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    ErrorMessage ?? $"El campo {validationContext.DisplayName} debe tener al menos {_minLength} elemento(s).",
                    new[] { validationContext.MemberName! }
                );
            }
        }

        if (_minLength > 0)
        {
            return new ValidationResult(
                ErrorMessage ?? $"El campo {validationContext.DisplayName} debe tener al menos {_minLength} elemento(s).",
                new[] { validationContext.MemberName! }
            );
        }

        return ValidationResult.Success;
    }
}