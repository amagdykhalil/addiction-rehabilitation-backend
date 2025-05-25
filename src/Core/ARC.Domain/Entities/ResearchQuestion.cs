namespace ARC.Persistence.Entities
{
    public class ResearchQuestion : Entity
    {
        public string QuestionText_ar { get; set; }
        public string QuestionText_en { get; set; }
        public int ResearchTypeId { get; set; }
        public int? ParentQuestionId { get; set; }

        // Navigation Properties
        public ResearchType ResearchType { get; set; }
        public ResearchQuestion ParentQuestion { get; set; }
        public ICollection<ResearchQuestion> ChildQuestions { get; set; }
        public ICollection<QuestionVersion> QuestionVersions { get; set; }
    }
} 