using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
namespace Application.Extensions
{
    public static class UseCasesCollectionExtensions
    {
        public static void AddUseCaseControllers(this IServiceCollection services)
        {
            services.AddScoped<IAddOrGetCache, AddOrGetCache>();
        }
    }
}