using AutoMapper;
using FilmoSearch.Api.ViewModels;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    public class FilmController : GenericController<FilmViewModel, FilmModel>
    {
        [ApiController]
        [Route("[Controller]")]
        public FilmController(IGenericService<FilmModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
