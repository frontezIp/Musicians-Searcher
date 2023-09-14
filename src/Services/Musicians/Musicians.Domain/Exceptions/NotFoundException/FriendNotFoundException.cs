namespace Musicians.Domain.Exceptions.NotFoundException
{
    public class FriendNotFoundException : NotFoundException
    {
        public FriendNotFoundException(Guid musicianId, Guid fakeMusicianFriendId) 
            : base($"Musician with {fakeMusicianFriendId} id doesn't exists in the friend list of the user with {musicianId} id")
        {
        }
    }
}
