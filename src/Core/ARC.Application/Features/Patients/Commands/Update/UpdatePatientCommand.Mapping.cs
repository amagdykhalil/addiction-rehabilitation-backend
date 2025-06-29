namespace ARC.Application.Features.Patients.Commands.Update
{
    public static class UpdatePatientCommandMappingExtensions
    {
        public static void MapToEntities(this UpdatePatientCommand command, Patient patient)
        {
            patient.BirthDate = command.BirthDate;
            if (patient.Person != null)
            {
                patient.Person.FirstName = command.FirstName;
                patient.Person.SecondName = command.SecondName;
                patient.Person.ThirdName = command.ThirdName;
                patient.Person.LastName = command.LastName;
                patient.Person.Gender = command.Gender;
                patient.Person.CallPhoneNumber = command.CallPhoneNumber;
                patient.Person.NationalIdNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? null : command.NationalIdNumber;
                patient.Person.PassportNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? command.PassportNumber : null;
                patient.Person.NationalityId = command.NationalityId;
                patient.Person.PersonalImageURL = command.PersonalImageURL;
            }
        }
    }
}