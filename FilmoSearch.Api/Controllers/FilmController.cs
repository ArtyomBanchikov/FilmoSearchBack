using AutoMapper;
using FilmoSearch.Api.ViewModels.Film;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmController : GenericController<FilmViewModel, AddFilmViewModel, FilmModel>
    {
        public FilmController(IGenericService<FilmModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
