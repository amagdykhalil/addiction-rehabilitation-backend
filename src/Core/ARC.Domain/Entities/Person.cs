using ARC.Domain.Enums;

namespace ARC.Persistence.Entities
{

    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public enGender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string CallPhoneNumber { get; set; }
        public string? NationalIdNumber { get; set; }
        public string? PassportNumber { get; set; }
        public int NationalityId { get; set; }
        public string? PersonalImageURL { get; set; }
        public Country Nationality { get; set; }
    }

}



