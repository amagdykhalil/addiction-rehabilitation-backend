

namespace ARC.Application.Features.Users.Queries.GetById
{
    public static class UserDetailsDtoMappingExtensions
    {
        public static UserDetailsDto ToDto(this User user, List<string> roles)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            return new UserDetailsDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Person?.FirstName,
                SecondName = user.Person?.SecondName,
                ThirdName = user.Person?.ThirdName,
                LastName = user.Person?.LastName,
                Gender = user.Person?.Gender ?? default,
                CallPhoneNumber = user.Person?.CallPhoneNumber,
                NationalIdNumber = user.Person?.NationalIdNumber,
                PassportNumber = user.Person?.PassportNumber,
                NationalityId = user.Person?.NationalityId ?? 0,
                NationalityName = lang == "ar" ? user.Person.Nationality.Name_ar : user.Person.Nationality.Name_en,
                PersonalImageURL = user.Person?.PersonalImageURL,
                IsActive = user.DeletedAt == null,
                Roles = roles ?? new List<string>()
            };
        }
    }
}