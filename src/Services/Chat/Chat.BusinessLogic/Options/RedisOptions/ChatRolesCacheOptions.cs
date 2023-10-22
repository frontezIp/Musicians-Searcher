namespace Chat.BusinessLogic.Options.RedisOptions
{
    public class ChatRolesCacheOptions
    {
        public int AbsoluteExpirationMinutes { get; set; }
        public int? SlidingExpirationMinutes { get; set; }
    }
}
