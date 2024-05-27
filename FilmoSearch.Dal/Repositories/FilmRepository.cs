using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.Repositories
{
    public class FilmRepository : GenericRepository<FilmEntity>
    {
        public FilmRepository(FilmoContext context) : base(context)
        {
        }

        public async override Task<FilmEntity?> GetByIdAsync(int id, CancellationToken token)
        {
            return await dbSet.Include(f => f.Genres)
                .Include(f => f.Reviews)
                .Include(f => f.Actors)
                .FirstOrDefaultAsync(f => f.Id == id, token);
        }

        public async override Task<IEnumerable<FilmEntity>> GetAllAsync(CancellationToken token)
        {
            return await dbSet.AsNoTracking()
                .Include(f => f.Genres)
                .ToListAsync(token);
        }
    }
}
