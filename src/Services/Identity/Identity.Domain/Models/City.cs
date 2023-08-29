namespace Identity.Domain.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public Guid CountryId { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
