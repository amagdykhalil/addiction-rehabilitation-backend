using ARC.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARC.Domain.Entities
{

    public class ChildParent : Entity
    {
        public int AdmissionAssessmentId { get; set; }
        public int ParentPersonId { get; set; }
        public enParentType ParentType { get; set; }
        public int EmploymentStatusId { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public enEmploymentStatus EmploymentStatusEnum
        {
            get => (enEmploymentStatus)EmploymentStatusId;
            set => EmploymentStatusId = (int)value;
        }

        public Person ParentPerson { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; } // To be created
        // Navigation properties (to be implemented as needed)
    }


}