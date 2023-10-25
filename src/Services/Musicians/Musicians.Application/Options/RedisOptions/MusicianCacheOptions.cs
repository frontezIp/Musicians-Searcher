namespace Musicians.Application.Options.RedisOptions
{
    public class MusicianCacheOptions
    {
        public int AbsoluteExpirationMinutes { get; set; }
        public int? SlidingExpirationMinutes { get; set; }
    }
}
