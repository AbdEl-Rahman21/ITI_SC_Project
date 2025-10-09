using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateGreaterThanAttribute(string otherPropertyName) : ValidationAttribute
    {
        private readonly string otherPropertyName = otherPropertyName;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = value as DateOnly?;

            var otherProperty = validationContext.ObjectType.GetProperty(otherPropertyName);

            if (otherProperty == null) return new ValidationResult($"Unknown property: {otherPropertyName}");

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance) as DateOnly?;

            if (currentValue != null && otherValue != null && currentValue <= otherValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be after {otherPropertyName}.");
            }

            return ValidationResult.Success;
        }
    }
}
