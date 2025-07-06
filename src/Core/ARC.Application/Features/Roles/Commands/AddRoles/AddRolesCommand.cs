using ARC.Application.Features.Roles.Models;

namespace ARC.Application.Features.Roles.Commands.AddRoles
{
    public record AddRolesCommand(List<RoleDto> Roles) : ICommand<bool>;
}