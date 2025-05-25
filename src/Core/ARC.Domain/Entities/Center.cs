namespace ARC.Persistence.Entities
{
    public class Center : Entity
    {
        public string Code { get; set; }
        public string Name_ar { get; set; }
        public string Name_en { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public State State { get; set; }
        public City City { get; set; }
    }
} 