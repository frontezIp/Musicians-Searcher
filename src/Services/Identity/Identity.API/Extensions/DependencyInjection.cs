using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

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
