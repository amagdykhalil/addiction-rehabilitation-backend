namespace ARC.Application.Features.Users.Commands.Create
{
    public static class CreateUserCommandMappingExtensions
    {
        public static Person MapToPerson(this CreateUserCommand command)
        {
            return new Person
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
        }

        public static User MapToUser(this CreateUserCommand command, int personId = 0)
        {
            return new User
            {
                Person = command.MapToPerson(),
                PersonId = personId,
                Email = command.Email,
                UserName = command.Email // Email and UserName are the same in this system
            };
        }
    }
}