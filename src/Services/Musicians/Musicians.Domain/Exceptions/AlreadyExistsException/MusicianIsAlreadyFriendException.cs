namespace Musicians.Domain.Exceptions.AlreadyExistsException
{
    public class MusicianIsAlreadyFriendException : AlreadyExistsException
    {
        public MusicianIsAlreadyFriendException(Guid musicianId, Guid musicianToAddId) 
            : base($"Musician with {musicianToAddId} id is already in the friends list of the user with {musicianId} id")
        {
        }
    }
}
