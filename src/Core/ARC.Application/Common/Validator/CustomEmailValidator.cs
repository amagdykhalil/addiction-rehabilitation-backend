using System.Text.RegularExpressions;

namespace ARC.Application.Common.Validator
{
    public class CustomEmailValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "EmailValidator";

        private readonly IStringLocalizer _localizer;
        private readonly bool _requered;
        private const int _maxLength = 256;
        private static readonly Regex _emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);



        public CustomEmailValidator(IStringLocalizer localizer, bool requered = false)
        {
            _localizer = localizer;
            _requered = requered;
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (_requered && string.IsNullOrEmpty(value))
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.Required]);
                return false;
            }


            if (value.Length > _maxLength)
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.MustBeLessThanOrEquel, _maxLength]);
                return false;
            }

            if (!_emailRegex.IsMatch(value))
            {
                // Ensure LocalizationKeys.Validation.InvalidEmailFormat exists in your localization keys
                context.AddFailure(_localizer[LocalizationKeys.Validation.InvalidFormat]);
                return false;
            }

            return true;
        }
    }
}

