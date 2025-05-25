namespace ARC.Persistence.Entities
{
    public class ResearchAnswerChoice : Entity
    {
        public int ResearchQuestionAnswerId { get; set; }
        public int ResearchQuestionChoiceId { get; set; }

        // Navigation properties
        public ResearchQuestionAnswer ResearchQuestionAnswer { get; set; }
        public ResearchQuestionChoice ResearchQuestionChoice { get; set; }
    }
} 