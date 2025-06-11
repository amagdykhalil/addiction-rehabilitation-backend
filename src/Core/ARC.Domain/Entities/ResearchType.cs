namespace ARC.Domain.Entities
{
    public class ResearchType : Entity
    {
        public string Name_ar { get; set; }
        public string Name_en { get; set; }
        public int? ParentResearchTypeId { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public ResearchType ParentResearchType { get; set; }
        public ICollection<ResearchType> ChildResearchTypes { get; set; }
        public ICollection<Research> Researches { get; set; }
        public ICollection<ResearchQuestion> ResearchQuestions { get; set; }
    }
} 