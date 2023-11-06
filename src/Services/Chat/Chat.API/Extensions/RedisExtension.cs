namespace Chat.API.Extensions
{
    public static class RedisExtension
    {
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("redis");
                options.InstanceName = configuration["ServiceName"];
            });
        }
    }
}
