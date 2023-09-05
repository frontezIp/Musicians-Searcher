using Identity.API.IdentityServerConfig;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;

namespace Identity.API.Extensions
{
    public static class IdentityExtension
    {

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentity();
            services.ConfigureIdentityServer(configuration);
            services.ConfigureAuthentication(configuration);
            services.ConfigureAuthorization();
        }

        private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["IdentityServer:IssuerUri"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });
        }

        private static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(builder =>
            {
                builder.AddPolicy(AvailablePolicies.AdminOnlyPolicy, pb =>
                {
                    pb.RequireAuthenticatedUser()
                        .RequireRole(RoleNamesConstants.AdminRoleName);
                });
            });
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            ;
        }

        private static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer(opt =>
            {
                opt.IssuerUri = configuration["IdentityServer:IssuerUri"];
            })
                .AddAspNetIdentity<User>()
                .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                .AddInMemoryApiResources(Configuration.GetApis())
                .AddInMemoryApiScopes(Configuration.GetApiScopes())
                .AddInMemoryClients(Configuration.GetClients())
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();
        }
    }
}
