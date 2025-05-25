namespace ARC.Persistence.Entities
{
    public class Patient : AuditableSoftDeleteEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}