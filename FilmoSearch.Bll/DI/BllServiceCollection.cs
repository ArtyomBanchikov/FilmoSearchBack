using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using FilmoSearch.Bll.Services;
using FilmoSearch.Dal.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearch.Bll.DI
{
    public static class BLLServiceCollection
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenericService<ActorModel>, ActorService>();
            services.AddScoped<IGenericService<FilmModel>, FilmService>();
            services.AddScoped<IGenericService<GenreModel>, GenreService>();
            services.AddScoped<IGenericService<ReviewModel>, ReviewService>();
            services.AddScoped<IGenericService<UserModel>, UserService>();

            services.AddDalServices(configuration);
        }
    }
}
