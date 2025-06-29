namespace ARC.Domain.Entities
{
    public class Patient : AuditableSoftDeleteEntity
    {
        public int PersonId { get; set; }
        public DateOnly BirthDate { get; set; }
        public Person Person { get; set; }
    }
}