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


            services.Scan(scan => scan
                .FromAssemblyOf<IUserRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddScoped<IBackendDbContext, BackendDbContext>();
        }
    }
}