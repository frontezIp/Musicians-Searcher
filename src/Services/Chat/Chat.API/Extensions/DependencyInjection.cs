namespace Сhat.API.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureAPI(this IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}
