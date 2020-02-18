using System.Web.Http;
using AutoMapper;

namespace EVE.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
