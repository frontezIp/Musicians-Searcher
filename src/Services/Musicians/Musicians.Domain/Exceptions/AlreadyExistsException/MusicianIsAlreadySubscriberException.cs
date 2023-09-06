namespace Musicians.Domain.Exceptions.AlreadyExistsException
{
    public class MusicianIsAlreadySubscriberException : AlreadyExistsException
    {
        public MusicianIsAlreadySubscriberException(Guid musicianId, Guid musicianToSubscribeId) 
            : base($"Musician with {musicianToSubscribeId} already has musician with {musicianId} in the subscribers list")
        {
        }
    }
}
