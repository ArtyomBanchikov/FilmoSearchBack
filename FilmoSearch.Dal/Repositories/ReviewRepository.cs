using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Dal.Repositories
{
    public class ReviewRepository : GenericRepository<ReviewEntity>
    {
        public ReviewRepository(FilmoContext context) : base(context)
        {
        }
    }
}
