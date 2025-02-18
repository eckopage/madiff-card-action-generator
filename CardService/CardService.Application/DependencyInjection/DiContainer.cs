using CardService.Application.Interfaces;
using CardService.Api.Mappings;
using CardService.Application.Validators;
using FluentValidation;
using MediatR;

namespace CardService.Application
{
    public static class DiContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ICardService).Assembly));
            services.AddAutoMapper(typeof(CardMappingProfile));
            services.AddValidatorsFromAssemblyContaining<GetCardDetailsQueryValidator>();
            return services;
        }
    }
}