using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.DI;
using FilmoSearch.Dal.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearch.Bll.DI
{
    public static class BLLServiceCollection
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GenericService<ActorModel, ActorEntity>, ActorService>();
            services.AddScoped<GenericService<FilmModel, FilmEntity>, FilmService>();
            services.AddScoped<GenericService<ReviewModel, ReviewEntity>, ReviewService>();

            services.AddDalServices(configuration);
        }
    }
}
