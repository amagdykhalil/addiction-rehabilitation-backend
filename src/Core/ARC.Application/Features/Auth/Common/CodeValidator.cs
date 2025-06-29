namespace ARC.Application.Features.Auth.Common
{
    public class CodeValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "CodeValidator";

        private readonly IStringLocalizer _localizer;
        private const int MaxLength = 400;

        public CodeValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.Required]);
                return false;
            }

            if (value.Length > MaxLength)
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.MustBeLessThanOrEquel, MaxLength]);
                return false;
            }

            return true;
        }
    }
}
