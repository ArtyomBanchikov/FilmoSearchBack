using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Dal.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        public GenreRepository(FilmoContext context) : base(context)
        {
        }
    }
}
