using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using EVE.ApiModels.Catalog;
using EVE.Bussiness;
using EVE.Commons;
using EVE.Data;
using EVE.WebApi.Shared;
using EVE.WebApi.Shared.Response;

namespace EVE.WebApi.Controllers
{
    [RoutePrefix("EvalType")]
    public class EvalTypeController : BaseController
    {
        private readonly IEvalTypeBE EvalTypeBE;
        public EvalTypeController(IEvalTypeBE _EvalTypeBE,
                               IMapper mapper) : base(mapper)
        {
            EvalTypeBE = _EvalTypeBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalTypeBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalTypeBaseReq req)
        {
            var obj =await EvalTypeBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalTypeNotExist));
        }

    }
}
