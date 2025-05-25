namespace ARC.Persistence.Entities
{
    public class ResearchQuestionAnswer : SoftDeletableEntity
    {
        public int ResearchId { get; set; }
        public int QuestionVersionId { get; set; }
        public bool IsDependant { get; set; } // BIT
        public int? ChoiceId { get; set; }
        public decimal? NumericAnswer { get; set; }
        public string? FreeText { get; set; }
        public bool? BooleanAnswer { get; set; } // BIT

        // Navigation Properties
        public Research Research { get; set; }
        public QuestionVersion QuestionVersion { get; set; }
        public ResearchQuestionChoice? Choice { get; set; } // Assuming ChoiceId links to ResearchQuestionChoice
        public ICollection<ResearchAnswerChoice> ResearchAnswerChoices { get; set; }
    }
}