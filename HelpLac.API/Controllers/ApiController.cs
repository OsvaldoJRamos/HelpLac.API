using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HelpLac.API.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public ApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
