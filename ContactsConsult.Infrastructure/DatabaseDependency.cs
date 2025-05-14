using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsConsult.Infrastructure.Data;
using FIAP.TechChallenge.ContactsConsult.Infrastructure.ElasticSearch;
using FIAP.TechChallenge.ContactsConsult.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace FIAP.TechChallenge.ContactsConsult.Infrastructure
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IContactRepository, ContactRepository>();

            service.Configure<ElasticSettings>(configuration.GetSection("ElasticSettings"));
            service.AddSingleton<IElasticSettings>(sp => sp.GetRequiredService<IOptions<ElasticSettings>>().Value);
            service.AddSingleton(typeof(IElasticClient<>), typeof(ElasticClient<>));

            return service;
        }

        public static IServiceCollection AddDbContextDependency(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<ContactsDbContext>(options => options.UseMySql(connectionString,
                                                               new MySqlServerVersion(new Version(8, 0, 21)),
                                                               mySqlOptions => mySqlOptions.MigrationsAssembly("FIAP.TechChallenge.ContactsConsult.Infrastructure")));

            return service;
        }
    }
}
