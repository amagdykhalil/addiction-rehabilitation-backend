using ARC.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARC.Persistence.Entities
{
    public class PatientPlacement : AuditableSoftDeleteEntity
    {
        public int AdmissionAssessmentId { get; set; }
        public int PlacementId { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public enPlacement PlacementEnum
        {
            get => (enPlacement)PlacementId;
            set => PlacementId = (int)value;
        }

        // Navigation Properties
        public AdmissionAssessment AdmissionAssessment { get; set; }
        public Placement Placement { get; set; }
    }
}