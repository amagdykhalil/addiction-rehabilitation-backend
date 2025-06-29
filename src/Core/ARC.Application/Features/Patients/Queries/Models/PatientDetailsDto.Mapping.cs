namespace ARC.Application.Features.Patients.Queries.Models
{
    /// <summary>
    /// Extension methods for mapping from Patient entity to PatientDetailsDto.
    /// </summary>
    public static class PatientDetailsDtoMappingExtensions
    {
        public static PatientDetailsDto ToDto(this Patient patient)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            return new PatientDetailsDto
            {
                Id = patient.Id,
                BirthDate = patient.BirthDate,
                FirstName = patient.Person.FirstName,
                SecondName = patient.Person.SecondName,
                ThirdName = patient.Person.ThirdName,
                LastName = patient.Person.LastName,
                Gender = patient.Person.Gender,
                CallPhoneNumber = patient.Person.CallPhoneNumber,
                NationalIdNumber = patient.Person.NationalIdNumber,
                PassportNumber = patient.Person.PassportNumber,
                NationalityId = patient.Person.NationalityId,
                NationalityName = lang == "ar" ? patient.Person.Nationality.Name_ar : patient.Person.Nationality.Name_en,
                PersonalImageURL = patient.Person.PersonalImageURL
            };
        }
    }
}