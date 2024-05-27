using AutoMapper;
using FilmoSearch.Api.ViewModels.Genre;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GenreController : GenericController<GenreViewModel, AddGenreViewModel, GenreModel>
    {
        public GenreController(IGenericService<GenreModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
