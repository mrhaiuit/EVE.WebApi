using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.Bussiness;
using EVE.Commons;
using EVE.Data;
using EVE.WebApi.Authentication.Helper;
using EVE.WebApi.Shared;
using EVE.WebApi.Shared.Response;

namespace EVE.WebApi.Controllers
{
    [RoutePrefix("EvalResult")]
    public class EvalResultController : BaseController
    {
        private readonly IEvalResultBE EvalResultBE;
        public EvalResultController(IEvalResultBE _EvalResultBE,
                               IMapper mapper) : base(mapper)
        {
            EvalResultBE = _EvalResultBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalResultBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EvalResultGetByIdReq req)
        {
            var obj = EvalResultBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalResultInsertReq req)
        {
            var existobj = await EvalResultBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultHasExist));
            }

            EvalResultBE.Insert(Mapper.Map<EvalResult>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalResultUpdateReq req)
        {
            var obj = await EvalResultBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
            }

            Mapper.Map(req, obj);

            EvalResultBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalResultDeleteReq req)
        {
            var obj = await EvalResultBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
            }

            EvalResultBE.Delete(obj);

            return this.OkResult();
        }
    }
}
