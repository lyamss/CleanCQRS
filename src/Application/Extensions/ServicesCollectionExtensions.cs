using Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServicesControllers(this IServiceCollection services)
        {
            services.AddScoped<IRegexUtils, RegexUtils>();
            services.AddSingleton<IConfigString, ConfigString>();
        }
    }
}