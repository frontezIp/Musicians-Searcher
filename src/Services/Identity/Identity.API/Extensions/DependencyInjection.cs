namespace Identity.API
{
    public static class DependencyInjection
    {
        public static void ConfigureAPI(this IServiceCollection services) 
        {
            services.AddControllers();
        }


    }
}
