using ARC.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ARC.Application.Abstractions.UserContext;

namespace ARC.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, bool>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        private readonly IStringLocalizer<UpdateRoleCommandHandler> _localizer;

        public UpdateRoleCommandHandler(
            RoleManager<Role> roleManager,
            ILogger<UpdateRoleCommandHandler> logger,
            IStringLocalizer<UpdateRoleCommandHandler> localizer)
        {
            _roleManager = roleManager;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(command.Id.ToString());
            if (role == null)
            {
                return Result.Error(_localizer[LocalizationKeys.Role.NotFound]);
            }

            role.Name = command.Name_en;
            role.Name_ar = command.Name_ar;
            role.NormalizedName = command.Name_en.ToUpperInvariant();

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return Result.Error(_localizer[LocalizationKeys.Role.UpdateFail]);
            }

            _logger.LogInformation("Updated role {RoleId}: {Name_en} / {Name_ar}", command.Id, command.Name_en, command.Name_ar);
            return Result.Success(true);
        }
    }
}
