using AutoMapper;
using FilmoSearch.Api.ViewModels;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReviewController : GenericController<ReviewViewModel, ReviewModel>
    {
        public ReviewController(IGenericService<ReviewModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
