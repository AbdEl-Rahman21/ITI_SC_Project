using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var phoneString = value as string;

            if (string.IsNullOrWhiteSpace(phoneString)) return ValidationResult.Success;

            var phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                var number = phoneUtil.Parse(phoneString, null);

                if (!phoneUtil.IsValidNumber(number)) return new ValidationResult("Invalid phone number format.");

                var formatted = phoneUtil.Format(number, PhoneNumberFormat.E164);

                validationContext.ObjectType.GetProperty(validationContext.MemberName)?.SetValue(validationContext.ObjectInstance, formatted);

                return ValidationResult.Success;
            }
            catch (NumberParseException)
            {
                return new ValidationResult("Invalid phone number format.");
            }
        }
    }
}
