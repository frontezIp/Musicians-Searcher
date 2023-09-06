namespace Musicians.API.Extensions
{
    public static class AuthorizationExtension
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
        }
    }
}
