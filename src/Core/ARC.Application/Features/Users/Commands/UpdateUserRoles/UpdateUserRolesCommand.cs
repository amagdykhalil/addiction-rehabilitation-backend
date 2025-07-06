namespace ARC.Application.Features.Users.Commands.UpdateUserRoles
{
    public record UpdateUserRolesCommand(int UserId, List<int> RoleIds) : ICommand<bool>;
}