

namespace ARC.Application.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand(int Id, string Name_en, string Name_ar) : ICommand<bool>;
}
