namespace Musicians.Domain.Exceptions.NotFoundException
{
    public class MusicianNotFoundException : NotFoundException
    {
        public MusicianNotFoundException() 
            : base("Such musician does not exists")
        {
        }
    }
}
