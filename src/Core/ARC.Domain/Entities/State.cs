namespace ARC.Domain.Entities
{
    public class State : Entity
    {
        public int CountryId { get; set; }
        public string Name_en { get; set; }
        public string Name_ar { get; set; }
        public ICollection<City> Cities { get; set; }
        public Country Country { get; set; }
    }
}