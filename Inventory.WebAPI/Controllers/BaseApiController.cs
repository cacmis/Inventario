using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {
        //Puedes agregar la logica que se repite en los diferentes controllers
        protected readonly IMapper _mapper;
        public BaseApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}