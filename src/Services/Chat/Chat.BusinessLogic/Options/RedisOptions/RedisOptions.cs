namespace Chat.BusinessLogic.Options.RedisOptions
{
    public class RedisOptions
    {
        public ChatRolesCacheOptions ChatRolesCacheOptions { get; set; } = null!;
        public MessengerUserProfileCacheOptions MessengerUserProfileCacheOptions { get; set; } = null!;
    }
}
