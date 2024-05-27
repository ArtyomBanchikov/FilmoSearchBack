using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Dal.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>
    {
        public UserRepository(FilmoContext context) : base(context)
        {
        }
    }
}
