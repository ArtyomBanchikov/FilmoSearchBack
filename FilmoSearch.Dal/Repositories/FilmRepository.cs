using FilmoSearch.Dal.EF;
using FilmoSearch.Dal.Entity;

namespace FilmoSearch.Dal.Repositories
{
    public class FilmRepository : GenericRepository<FilmEntity>
    {
        public FilmRepository(FilmoContext context) : base(context)
        {
        }
    }
}
