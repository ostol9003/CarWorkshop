using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CReateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarWorkshopCommand)));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
                cfg.AddProfile(new CarWorkshopServiceMappingProfile(userContext));
            }).CreateMapper());

           

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            return services;
        }
    }
}
