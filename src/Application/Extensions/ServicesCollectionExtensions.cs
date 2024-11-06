using Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServicesControllers(this IServiceCollection services)
        {
            services.AddSingleton<IConfigString, ConfigString>();
            services.AddScoped<IEmailDtoValidator, EmailDtoValidator>();
            services.AddScoped<IIdDtoValidator, IdDtoValidator>();
            services.AddScoped<INameDtoValidator, NameDtoValidator>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IPriceValidator, PriceValidator>();
            services.AddScoped<IDescriptionDtoValidator, DescriptionDtoValidator>();
            services.AddScoped<ICreateUserCommandValidator, CreateUserCommandValidator>();
            services.AddScoped<IAddItemsCommandValidator, AddItemsCommandValidator>();
            //services.AddValidatorsFromAssemblyContaining<EmailDtoValidator>();
        }
    }
}