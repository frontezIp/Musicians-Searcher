using IdentityServer4.AccessTokenValidation;

namespace Musicians.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "Musicians";
                options.Authority = configuration["IdentityServer:IssuerUri"];
            });
        }
    }
}
