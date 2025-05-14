using FIAP.TechChallenge.ContactsConsult.Application;
using FIAP.TechChallenge.ContactsConsult.Domain;
using FIAP.TechChallenge.ContactsConsult.Infrastructure;

namespace FIAP.TechChallenge.ContactsConsult.Api.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositoriesDependency(configuration);
            services.AddDbContextDependency(configuration.GetConnectionString("DefaultConnection"));
            services.AddServicesDependency();
            services.AddApplicationDependency();
            services.AddAuthenticationDependency();
        }
    }
}
