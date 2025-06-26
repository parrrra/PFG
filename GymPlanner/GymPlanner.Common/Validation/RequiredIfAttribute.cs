using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Common.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RequiredIfAttribute : ValidationAttribute
{
    public string DependentProperty { get; }
    public object ExpectedValue { get; }

    public RequiredIfAttribute(string dependentProperty, object expectedValue)
    {
        DependentProperty = dependentProperty;
        ExpectedValue = expectedValue;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(DependentProperty);
        if (property == null)
        {
            return new ValidationResult($"Propiedad dependiente '{DependentProperty}' no encontrada.");
        }

        var dependentValue = property.GetValue(validationContext.ObjectInstance);

        if (Equals(dependentValue, ExpectedValue))
        {
            var isEmpty = value == null || (value is string str && string.IsNullOrWhiteSpace(str));

            if (isEmpty)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{validationContext.DisplayName} es obligatorio.",
                    [validationContext.MemberName!]
                );
            }
        }

        return ValidationResult.Success;
    }
}
