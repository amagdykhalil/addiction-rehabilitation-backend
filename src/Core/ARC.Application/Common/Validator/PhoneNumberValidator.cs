using FluentValidation.Validators;

namespace ARC.Application.Common.Validator
{
    public class PhoneNumberValidator<T> : PropertyValidator<T, string>
    {
        private readonly IStringLocalizer _localizer;
        public override string Name => "PhoneNumberValidator";

        public PhoneNumberValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.Required]);
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, "^(010|011|012|015)[0-9]{8}$"))
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.InvalidFormat]);
                return false;
            }
            return true;
        }
    }
}