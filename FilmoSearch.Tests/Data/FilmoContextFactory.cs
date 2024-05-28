using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Tests.Data
{
    public class FilmoContextFactory
    {
        public static FilmoContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<FilmoContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            var context = new FilmoContext(options);
            context.Database.EnsureCreated();

            context.SaveChanges();
            return context;
        }

        public static void Destroy(FilmoContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
