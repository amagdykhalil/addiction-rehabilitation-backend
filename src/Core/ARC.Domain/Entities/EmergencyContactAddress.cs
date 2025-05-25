namespace ARC.Persistence.Entities
{
    public class EmergencyContactAddress : Entity
    {
        public int AddressId { get; set; }
        public string Notes { get; set; }
        public EmergencyContact EmergencyContacts { get; set; }
        public Address Address { get; set; } // To be created
    }
} 