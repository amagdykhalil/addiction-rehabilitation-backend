namespace ARC.Domain.Entities
{
    public class City : Entity
    {
        public int StateId { get; set; }
        public string Name_en { get; set; }
        public string Name_ar { get; set; }
        public State State { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
} 