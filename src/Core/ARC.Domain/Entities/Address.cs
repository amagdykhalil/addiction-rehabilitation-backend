namespace ARC.Persistence.Entities
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string PostalCode { get; set; }
        public City City { get; set; }
    }
}