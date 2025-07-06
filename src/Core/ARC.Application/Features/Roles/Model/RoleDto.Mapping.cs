namespace ARC.Application.Features.Roles.Models
{
    public static class RoleDtoMappingExtensions
    {
        public static RoleDto ToRoleDto(this Role role)
        {

            return new RoleDto
            {
                Id = role.Id,
                Name_en = role.Name,
                Name_ar = role.Name_ar,
            };
        }

        public static List<RoleDto> ToRoleDtoList(this IEnumerable<Role> roles)
        {
            return roles.Select(r => r.ToRoleDto()).ToList();
        }
    }
}