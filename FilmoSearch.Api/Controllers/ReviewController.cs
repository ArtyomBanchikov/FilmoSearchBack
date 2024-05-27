using AutoMapper;
using FilmoSearch.Api.ViewModels.Review;
using FilmoSearch.Bll.Interfaces;
using FilmoSearch.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReviewController : GenericController<ReviewViewModel, AddReviewViewModel, ReviewModel>
    {
        public ReviewController(IGenericService<ReviewModel> genericService, IMapper mapper) : base(genericService, mapper)
        {
        }
    }
}
