namespace Musicians.Domain.Exceptions.NotFoundException
{
    public class MusicianNotFoundInSubscribersException : NotFoundException
    {
        public MusicianNotFoundInSubscribersException(Guid musicianId, Guid notSubId)
            : base($"Musician with {notSubId} was not found in the subscribers list of the musician with {musicianId}")
        {
        }
    }
}
