using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repository;

namespace Infrastructure.Extensions
{
    public static class InfraCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string ConnexionDB)
        {
            services.AddDbContext<BackendDbContext>(options => 
            {
                options.UseNpgsql(ConnexionDB);
            });

            // repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
            
            // persistence
            services.AddScoped<IBackendDbContext, BackendDbContext>();
        }
    }
}