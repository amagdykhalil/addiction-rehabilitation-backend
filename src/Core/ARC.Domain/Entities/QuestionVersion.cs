using ARC.Domain.Abstract;
using ARC.Persistence.Entities;

namespace ARC.Persistence.Entities
{
    public class QuestionVersion : CreationTrackableSoftDeleteEntity 
    {
        public int ResearchQuestionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AnswerType { get; set; }
        public bool IsRequired { get; set; }
        public int? ParentChoiceId { get; set; }
        public bool? ParentBooleanValue { get; set; }

        // Navigation Properties
        public ResearchQuestionChoice ParentChoice { get; set; }
        public ResearchQuestion ResearchQuestion { get; set; }
        public ICollection<ResearchQuestionAnswer> ResearchQuestionAnswers { get; set; }
        public ICollection<ResearchQuestionChoice> ResearchQuestionChoices { get; set; }
    }
} 