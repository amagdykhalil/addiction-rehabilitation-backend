using ARC.Application.Abstractions.UserContext;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using ARC.Application.Abstractions.Persistence;

namespace ARC.Application.Features.Users.Commands.Reactivate
{
    public class ReactivateUserCommandHandler : ICommandHandler<ReactivateUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<ReactivateUserCommandHandler> _logger;
        private readonly IStringLocalizer<ReactivateUserCommandHandler> _localizer;
        private readonly IUnitOfWork _unitOfWork;

        public ReactivateUserCommandHandler(
            IIdentityService identityService,
            ILogger<ReactivateUserCommandHandler> logger,
            IStringLocalizer<ReactivateUserCommandHandler> localizer,
            IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ReactivateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByIdAsync(command.Id, cancellationToken);
            if (user == null)
            {
                return Result.Error(_localizer[LocalizationKeys.User.NotFoundById, command.Id]);
            }

            user.DeletedAt = null;
            user.DeletedBy = null;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Reactivated user with ID: {UserId}", command.Id);
            return Result.Success(true);
        }
    }
} 