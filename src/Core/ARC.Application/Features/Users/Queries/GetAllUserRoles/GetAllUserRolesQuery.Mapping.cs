namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public static class GetAllUserRolesQueryResponseExtensions
    {
        public static GetAllUserRolesQueryResponse ToUserRoleQueryResponse(this Role role, string lang)
        {

            return new GetAllUserRolesQueryResponse
            {
                Id = role.Id,
                Name = lang == "ar" ? role.Name_ar : role.Name,
            };
        }

        public static List<GetAllUserRolesQueryResponse> ToUserRoleQueryResponseList(this IEnumerable<Role> roles)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            return roles.Select(r => r.ToUserRoleQueryResponse(lang)).ToList();
        }
    }
} 