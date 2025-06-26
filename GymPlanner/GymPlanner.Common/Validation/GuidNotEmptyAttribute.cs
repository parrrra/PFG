using System.ComponentModel.DataAnnotations;

namespace GymPlanner.Common.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class GuidNotEmptyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guid)
            return guid != Guid.Empty;

        return false;
    }
}