using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using People.Application.Database;
using People.Application.Repositories.PeopleRepository;
using People.Application.Services;

namespace People.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IPeopleRepository, PeopleRepository>();
            services.AddSingleton<IPersonService, PersonService>();
            services.AddValidatorsFromAssemblyContaining<IApplicationMaker>(ServiceLifetime.Singleton);
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            //Add create that connection to postgress
            //This will be a singleton since the factory will return new connection every time
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(connectionString));

            //will only run once
            services.AddSingleton<DbInitializer>();

            return services;
        }
    }
}
