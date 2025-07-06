namespace ARC.Application.Features.Users.Commands.RemoveRoles
{
    public record RemoveRolesCommand(List<int> RoleIds) : ICommand<bool>;
}