using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.Repositories
{
    public class ActorRepository : GenericRepository<ActorEntity>
    {
        public ActorRepository(FilmoContext context) : base(context)
        {
        }

        public async override Task<ActorEntity?> GetByIdAsync(int id, CancellationToken token)
        {
            return await dbSet.Include(a => a.Films)
                .FirstOrDefaultAsync(a => a.Id == id, token);
        }
    }
}
