using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        public GenreRepository(FilmoContext context) : base(context)
        {
        }

        public async override Task<GenreEntity?> GetByIdAsync(int id, CancellationToken token)
        {
            return await dbSet.Include(g => g.Films)
                .FirstOrDefaultAsync(g => g.Id == id, token);
        }
    }
}
