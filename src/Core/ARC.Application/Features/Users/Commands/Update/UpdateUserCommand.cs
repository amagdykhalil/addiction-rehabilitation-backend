using ARC.Domain.Enums;

namespace ARC.Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : ICommand<bool>
    {
        public int Id { get; init; }

        // Person Details
        public string FirstName { get; init; }
        public string SecondName { get; init; }
        public string ThirdName { get; init; }
        public string LastName { get; init; }
        public enGender Gender { get; init; }
        public string CallPhoneNumber { get; init; }
        public string? NationalIdNumber { get; init; }
        public string? PassportNumber { get; init; }
        public int NationalityId { get; init; }
        public string? PersonalImageURL { get; init; }

    }
}