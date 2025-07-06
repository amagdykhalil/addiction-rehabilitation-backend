using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(IStringLocalizer<DeleteUserCommandValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<DeleteUserCommand>(validationLocalizer));
        }
    }
} 