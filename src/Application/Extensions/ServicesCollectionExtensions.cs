using Application.Handlers.Authentification;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServicesControllers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IConfigStringSvs>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Svs")))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(EmailDtoValidator).Assembly);
        }
    }
}