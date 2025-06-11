namespace ARC.Domain.Entities
{
    public class Research : Entity
    {
        public int AdmissionAssessmentId { get; set; }
        public int ResearchTypeId { get; set; }

        // Navigation properties
        public AdmissionAssessment AdmissionAssessment { get; set; }
        public ResearchType ResearchType { get; set; }
        public ICollection<ResearchQuestionAnswer> ResearchQuestionAnswers { get; set; }
    }
} 