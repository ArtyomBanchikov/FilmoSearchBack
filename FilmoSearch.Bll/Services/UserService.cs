using AutoMapper;
using FilmoSearch.Bll.Models;
using FilmoSearch.Dal.Entity;
using FilmoSearch.Dal.Interfaces;

namespace FilmoSearch.Bll.Services
{
    public class UserService : GenericService<UserModel, UserEntity>
    {
        public UserService(IGenericRepository<UserEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}
