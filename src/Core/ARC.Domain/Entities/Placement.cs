namespace ARC.Persistence.Entities
{
    public class Placement : Entity
    {
        public string Name_ar { get; set; }
        public string Name_en { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<PatientPlacement> PatientPlacements { get; set; }
    }
} 