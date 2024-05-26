using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Dal.Repositories
{
    public class ActorRepository : GenericRepository<ActorEntity>
    {
        public ActorRepository(FilmoContext context) : base(context)
        {
        }
    }
}
