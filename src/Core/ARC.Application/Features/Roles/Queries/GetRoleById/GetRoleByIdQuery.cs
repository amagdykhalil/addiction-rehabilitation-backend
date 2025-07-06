using ARC.Application.Features.Roles.Models;

namespace ARC.Application.Features.Roles.Queries.GetRoleById
{
    public record GetRoleByIdQuery : IQuery<RoleDto>
    {
        public int Id { get; set; }
    }
}
