using Domain.Mappers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions
{
    public static class MappersCollectionExtensions
    {
        public static void MappersExtentedInjec(this IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
            .FromAssemblyOf<UserMapper>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Mapper")))
            .AsSelf()
            .WithScopedLifetime());
        }
    }
}