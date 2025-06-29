namespace ARC.Application.Common.Validator
{
    public class IdNullableValidator<T> : PropertyValidator<T, int?>
    {
        public override string Name => "IdNullableValidator";

        private readonly IStringLocalizer _localizer;

        public IdNullableValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public override bool IsValid(ValidationContext<T> context, int? value)
        {
            if (value == null)
                return true; // Not our concern if empty

            if (value <= 1)
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.MustBeGreaterThanOrEquel, 1]);
                return false;
            }

            return true;
        }
    }
}

