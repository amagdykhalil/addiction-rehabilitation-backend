using ARC.Application.Features.Users.Queries.GetById;

namespace ARC.Application.Features.Users.Queries.Models
{
    public static class UserWithRolesDtoMappingExtensions
    {
        public static UserDetailsDto ToUserDetailsDto(this UserWithRolesDto userWithRoles)
        {
            var roles = string.IsNullOrEmpty(userWithRoles.Roles)
                ? new List<string>()
                : userWithRoles.Roles.Split(',').Select(r => r.Trim()).ToList();

            return new UserDetailsDto
            {
                Id = userWithRoles.Id,
                Email = userWithRoles.Email,
                FirstName = userWithRoles.FirstName,
                SecondName = userWithRoles.SecondName,
                ThirdName = userWithRoles.ThirdName,
                LastName = userWithRoles.LastName,
                Gender = userWithRoles.Gender,
                CallPhoneNumber = userWithRoles.CallPhoneNumber,
                NationalIdNumber = userWithRoles.NationalIdNumber,
                PassportNumber = userWithRoles.PassportNumber,
                NationalityId = userWithRoles.NationalityId,
                NationalityName = userWithRoles.NationalityName,
                PersonalImageURL = userWithRoles.PersonalImageURL,
                IsActive = userWithRoles.DeletedAt == null,
                Roles = roles
            };
        }
    }
}