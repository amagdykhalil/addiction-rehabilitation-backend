namespace ARC.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator(IStringLocalizer<RefreshTokenCommand> stringLocalizer)
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(stringLocalizer[LocalizationKeys.Auth.InvalidToken])
                .MaximumLength(1000)
                .WithMessage(stringLocalizer[LocalizationKeys.Auth.InvalidToken]);
        }
    }
}
