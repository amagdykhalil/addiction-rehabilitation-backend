namespace ARC.Application.Features.Patients.Commands.Create
{
    /// <summary>
    /// Extension methods for mapping from and to Patient and Person entities.
    /// </summary>
    public static class CreatePatientCommandMappingExtensions
    {
        public static Person MapToPerson(this CreatePatientCommand command)
            => new Person
            {
                FirstName = command.FirstName,
                SecondName = command.SecondName,
                ThirdName = command.ThirdName,
                LastName = command.LastName,
                Gender = command.Gender,
                CallPhoneNumber = command.CallPhoneNumber,
                NationalIdNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? null : command.NationalIdNumber,
                PassportNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? command.PassportNumber : null,
                NationalityId = command.NationalityId,
                PersonalImageURL = command.PersonalImageURL
            };

        public static Patient MapToPatient(this CreatePatientCommand command, int personId = 0)
            => new Patient
            {
                PersonId = personId,
                BirthDate = command.BirthDate,
                Person = command.MapToPerson()
            };
    }
}