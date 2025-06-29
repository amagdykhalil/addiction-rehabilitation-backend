using ARC.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARC.Domain.Entities
{

    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}".Trim();
            }
        }
        public enGender Gender { get; set; }
        public string CallPhoneNumber { get; set; }
        public string? NationalIdNumber { get; set; }
        public string? PassportNumber { get; set; }
        public int NationalityId { get; set; }
        public string? PersonalImageURL { get; set; }
        public Country Nationality { get; set; }
    }

}



