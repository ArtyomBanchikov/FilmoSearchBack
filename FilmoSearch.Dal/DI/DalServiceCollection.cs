using FilmoSearch.Dal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmoSearch.Dal.DI
{
    public static class DalServiceCollection
    {
        public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<FilmoContext>(c => c.UseNpgsql(dbConnection));
        }
    }
}
