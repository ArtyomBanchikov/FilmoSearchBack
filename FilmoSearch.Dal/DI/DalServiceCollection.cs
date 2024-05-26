using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearch.Dal.DI
{
    public static class DalServiceCollection
    {
        public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FilmoContext>(c => 
                c.UseNpgsql(configuration.GetConnectionString("DbConnection")));

            services.AddScoped<GenericRepository<ActorEntity>, ActorRepository>();
            services.AddScoped<GenericRepository<FilmEntity>, FilmRepository>();
            services.AddScoped<GenericRepository<ReviewEntity>, ReviewRepository>();
        }
    }
}
