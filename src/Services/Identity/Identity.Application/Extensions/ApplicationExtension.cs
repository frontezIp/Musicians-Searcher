using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Application.Interfaces.ServiceHelpers;
using Identity.Application.Interfaces.Services;
using Identity.Application.Services;
using Identity.Application.Validators;
using Identity.Application.Validators.ServiceHelpers;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddMappings();
            services.ConfigureServiceValidators();
            services.ConfigureServices();
            services.ConfigureFluentValidation();   
        }

        private static void ConfigureServices(this IServiceCollection services) 
        { 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICountryService, CountryService>();
        }

        private static void ConfigureServiceValidators(this IServiceCollection services)
        {
            services.AddScoped<IUserServiceHelper, UserServiceHelper>();
            services.AddScoped<ICityServiceHelper, CityServiceHelper>();
        }

        private static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(ValidatorsAssemblyReference.Assembly);
        }

        private static void AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
