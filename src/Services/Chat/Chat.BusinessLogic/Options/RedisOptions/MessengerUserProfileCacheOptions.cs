namespace Chat.BusinessLogic.Options.RedisOptions
{
    public class MessengerUserProfileCacheOptions
    {
        public int AbsoluteExpirationMinutes { get; set; }
        public int? SlidingExpirationMinutes { get; set; }
    }
}
