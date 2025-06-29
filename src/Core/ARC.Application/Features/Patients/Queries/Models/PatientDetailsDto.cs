using ARC.Domain.Enums;

namespace ARC.Application.Features.Patients.Queries.Models
{
    public class PatientDetailsDto
    {
        public int Id { get; set; }
        public DateOnly BirthDate { get; set; }

        // Person Details
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
    }
}