using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repository.User;
using Infrastructure.Repository;

namespace Infrastructure.Extensions
{
    public static class InfraCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string ConnexionDB, string ConnexionRedis)
        {
            services.AddDbContext<BackendDbContext>(options => 
            {
                options.UseNpgsql(ConnexionDB);
            });

            // repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            
            // persistence
            services.AddScoped<IBackendDbContext, BackendDbContext>();
        }
    }
}