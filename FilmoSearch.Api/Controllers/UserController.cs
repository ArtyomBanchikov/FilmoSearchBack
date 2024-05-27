using AutoMapper;
using FilmoSearch.Api.ViewModels.User;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : GenericController<UserViewModel, AddUserViewModel, UserModel>
    {
        public UserController(IGenericService<UserModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
