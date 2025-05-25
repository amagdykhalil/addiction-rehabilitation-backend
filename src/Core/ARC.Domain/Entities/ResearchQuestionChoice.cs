namespace ARC.Persistence.Entities
{
    public class ResearchQuestionChoice : Entity
    {
        public int QuestionVersionId { get; set; }
        public string Choice_ar { get; set; }
        public string Choice_en { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public QuestionVersion QuestionVersion { get; set; }
        public ICollection<ResearchAnswerChoice> ResearchAnswerChoices { get; set; }
    }
} 