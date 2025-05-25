namespace ARC.Persistence.Entities
{
    public class Country : Entity
    {
        public string Name_en { get; set; }
        public string Name_ar { get; set; }
        public string Iso3 { get; set; }
        public string Code { get; set; }
        public ICollection<Person> People { get; set; }
    }
}