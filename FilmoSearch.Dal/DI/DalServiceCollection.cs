using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;
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

            services.AddScoped<IGenericRepository<ActorEntity>, ActorRepository>();
            services.AddScoped<IGenericRepository<FilmEntity>, FilmRepository>();
            services.AddScoped<IGenericRepository<GenreEntity>, GenreRepository>();
            services.AddScoped<IGenericRepository<ReviewEntity>, ReviewRepository>();
            services.AddScoped<IGenericRepository<UserEntity>, UserRepository>();
        }
    }
}
