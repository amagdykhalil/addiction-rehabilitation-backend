namespace ARC.Application.Common.Validator
{
    public class IdValidator<T> : PropertyValidator<T, int>
    {
        public override string Name => "IdValidator";

        private readonly IStringLocalizer _localizer;

        public IdValidator(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public override bool IsValid(ValidationContext<T> context, int value)
        {

            if (value <= 1)
            {
                context.AddFailure(_localizer[LocalizationKeys.Validation.MustBeGreaterThanOrEquel, 1]);
                return false;
            }

            return true;
        }
    }
}