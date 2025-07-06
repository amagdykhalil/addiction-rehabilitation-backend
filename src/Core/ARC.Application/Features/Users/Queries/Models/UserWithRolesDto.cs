using ARC.Domain.Enums;

namespace ARC.Application.Features.Users.Queries.Models
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int PersonId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public enGender Gender { get; set; }
        public string CallPhoneNumber { get; set; }
        public string? NationalIdNumber { get; set; }
        public string? PassportNumber { get; set; }
        public int NationalityId { get; set; }
        public string NationalityName { get; set; }
        public string? PersonalImageURL { get; set; }
        public string? Roles { get; set; }
        public int TotalCount { get; set; }
    }
}