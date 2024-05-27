using AutoMapper;
using FilmoSearch.Api.ViewModels.Actor;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ActorController : GenericController<ActorViewModel, AddActorViewModel, ActorModel>
    {
        public ActorController(IGenericService<ActorModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
