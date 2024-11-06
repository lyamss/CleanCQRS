using Infrastructure.Extensions;
using Application.Extensions;
using Infrastructure.Persistence;
using DotNetEnv;
using API.Filters;
using Domain.Extensions;
namespace API
{
    public class Startup()
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            Env.Load();
            Env.TraversePath().Load();

            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddServicesControllers();

            services.MappersExtentedInjec();

            services.AddInfrastructure(Env.GetString("ConnexionDB"));

            services.AddScoped<AuthorizeAuth>();

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