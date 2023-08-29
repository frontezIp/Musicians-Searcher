namespace Identity.Domain.Exceptions.NotFoundException
{
    public class CityNotFoundException : NotFoundException
    {
        public CityNotFoundException()
            : base("Such city doesn't exists") { }
    }
}
