using Domain.Mappers.AuthToken;
using Domain.Mappers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class MappersCollectionExtensions
    {
        public static void MappersExtentedInjec(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserMapper>();
            serviceCollection.AddScoped<AuthTokenMapper>();
        }
    }
}