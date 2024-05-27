using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>
    {
        public UserRepository(FilmoContext context) : base(context)
        {
        }

        public async override Task<UserEntity?> GetByIdAsync(int id, CancellationToken token)
        {
            return await dbSet.Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == id, token);
        }
    }
}
