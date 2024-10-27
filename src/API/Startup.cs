using Infrastructure.Extensions;
using Application.Extensions;
using StackExchange.Redis;
using Infrastructure.Persistence;
using DotNetEnv;
using Application.Handlers.Users;
namespace API
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services) 
        {
            Env.Load();
            Env.TraversePath().Load();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
            });

            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddUseCaseControllers();

            services.AddServicesControllers();

            services.MappersExtentedInjec();

            services.AddInfrastructure(Env.GetString("ConnexionDB"), Env.GetString("ConnexionRedis"));

            services.AddCors(options =>
            {
                options.AddPolicy(name: "web_site_Front", configurePolicy: policyBuilder =>
                {
                    policyBuilder.WithOrigins(Env.GetString("IP_NOW_FRONTEND"))
                    .WithHeaders("Content-Type")
                    .WithMethods("GET", "POST", "PATCH")
                    .AllowCredentials();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<IBackendDbContext>();
                dataContext.Migrate();

                var cacheDB = ConnectionMultiplexer.Connect(Env.GetString("ConnexionRedis"));
                if (!cacheDB.IsConnected)
                {
                    Console.WriteLine("Failed to connect to CacheDB, Exiting API :/");
                    Environment.Exit(1);
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            if(env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("web_site_Front");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}