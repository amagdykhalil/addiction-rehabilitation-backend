namespace ARC.Persistence.Entities
{
    public class EmergencyContact : Entity
    {
        public int AdmissionAssessmentId { get; set; }
        public int EmergentPersonId { get; set; }
        public int ContactAddressId { get; set; }
        public string Notes { get; set; }
        public Person EmergentPerson { get; set; }
        public EmergencyContactAddress ContactAddress { get; set; }

    }
}