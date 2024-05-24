using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.EF
{
    public  class FilmoContext : DbContext
    {
        public FilmoContext(DbContextOptions<FilmoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
