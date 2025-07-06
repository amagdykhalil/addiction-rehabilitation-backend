using ARC.Domain.Enums;

namespace ARC.Application.Features.Users.Commands.Create
{
    public record CreateUserCommand : ICommand<int>
    {
        // Person data
        public string FirstName { get; init; }
        public string SecondName { get; init; }
        public string? ThirdName { get; init; }
        public string LastName { get; init; }
        public enGender Gender { get; init; }
        public string CallPhoneNumber { get; init; }
        public string? NationalIdNumber { get; init; }
        public string? PassportNumber { get; init; }
        public int NationalityId { get; init; }
        public string? PersonalImageURL { get; init; }

        // User data
        public string Email { get; init; }

        // Additional data
        public List<int>? Roles { get; init; }
    }
}