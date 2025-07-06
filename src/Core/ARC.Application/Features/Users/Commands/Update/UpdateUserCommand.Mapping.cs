namespace ARC.Application.Features.Users.Commands.Update
{
    public static class UpdateUserCommandMappingExtensions
    {
        public static Person MapToPerson(this UpdateUserCommand command)
        {
            return new Person
            {
                Id = 0, // Will be set by the handler
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
        }

        public static void MapToExistingPerson(this UpdateUserCommand command, Person person)
        {
            person.FirstName = command.FirstName;
            person.SecondName = command.SecondName;
            person.ThirdName = command.ThirdName;
            person.LastName = command.LastName;
            person.Gender = command.Gender;
            person.CallPhoneNumber = command.CallPhoneNumber;
            person.NationalIdNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? null : command.NationalIdNumber;
            person.PassportNumber = string.IsNullOrEmpty(command.NationalIdNumber) ? command.PassportNumber : null;
            person.NationalityId = command.NationalityId;
            person.PersonalImageURL = command.PersonalImageURL;
        }

        public static void MapToExistingUser(this UpdateUserCommand command, User user)
        {
            command.MapToExistingPerson(user.Person);
        }
    }
}