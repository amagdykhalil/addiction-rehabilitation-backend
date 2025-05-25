namespace ARC.Domain.Interfaces
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted => DeletedAt.HasValue;
        public DateTime? DeletedAt { get; set; }
        int? DeletedBy { get; set; }
    }
}



