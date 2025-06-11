namespace ARC.Domain.Entities
{
    public class ResidenceInfo : Entity
    {
        public int? AddressId { get; set; }
        public bool HasFixedResidence { get; set; }
        public string Notes { get; set; }
        public Address Address { get; set; }
    }
} 