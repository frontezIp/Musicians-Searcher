namespace Identity.Domain.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<City> Cities { get; set; } = new();
    }
}
