using ARC.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARC.Domain.Entities
{
    public class AdmissionAssessment : AuditableSoftDeleteEntity
    {
        public int CenterId { get; set; }
        public int PatientId { get; set; }
        public DateTime InterviewDate { get; set; }
        public enMaritalStatus MaritalStatus { get; set; }
        public enEducationalLevel EducationalLevel { get; set; }
        public int EmploymentStatusId { get; set; }
        public int ResidenceInfoId { get; set; }

        [NotMapped]
        public enEmploymentStatus EmploymentStatusEnum
        {
            get => (enEmploymentStatus)EmploymentStatusId;
            set => EmploymentStatusId = (int)value;
        }

        // Navigation Properties
        public Center Center { get; set; }
        public Patient Patient { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public ResidenceInfo ResidenceInfo { get; set; }
    }
}