﻿using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Musicians.Application.Mappings;
using Musicians.Application.Validators;
using Musicians.Application.MediatoR.Features;
using Musicians.Application.MediatoR.Behaviours;
using MapsterMapper;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Application.ServiceHelpers;

namespace Musicians.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.ConfigureMappings();
            services.ConfigureServiceValidators();
            services.ConfigureFluentValidators();
            services.ConfigureMediatr();
        }

        private static void ConfigureMediatr(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(ServicesAssemblyReference.Assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
        }

        private static void ConfigureMappings(this IServiceCollection serviceCollection)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(MappingsAssemblyReference.Assembly);
            serviceCollection.AddSingleton(config);
            serviceCollection.AddScoped<IMapper, ServiceMapper>();
        }

        private static void ConfigureFluentValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(ValidatorsAssemblyReference.Assembly);
        }

        private static void ConfigureServiceValidators(this IServiceCollection services)
        {
            services.AddScoped<IMusicianServiceHelper, MusicianServiceHelper>();
            services.AddScoped<ISkillServiceHelper, SkillServiceHelper>();
            services.AddScoped<IGenreServiceHelper, GenreServiceHelper>();
        }
    }
}
