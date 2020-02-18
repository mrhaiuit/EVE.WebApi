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
    [RoutePrefix("EvalStandard")]
    public class EvalStandardController : BaseController
    {
        private readonly IEvalStandardBE EvalStandardBE;
        public EvalStandardController(IEvalStandardBE _EvalStandardBE,
                               IMapper mapper) : base(mapper)
        {
            EvalStandardBE = _EvalStandardBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalStandardBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("GetEvalCriteriaByStandard")]
        public HttpResponseMessage GetEvalCriteriaByStandard([FromUri] int StandardId)
        {
            var obj = EvalStandardBE.GetEvalCriteriaByStandard(StandardId);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.CateriaNotExistWithStandard));
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EvalStandardGetByIdReq req)
        {
            var obj = EvalStandardBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EvalStandardNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalStandardInsertReq req)
        {
            var existobj = await EvalStandardBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStandardHasExist));
            }

            EvalStandardBE.Insert(Mapper.Map<EvalStandard>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalStandardUpdateReq req)
        {
            var obj = await EvalStandardBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStandardNotExist));
            }

            Mapper.Map(req, obj);

            EvalStandardBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalStandardDeleteReq req)
        {
            var obj = await EvalStandardBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStandardNotExist));
            }

            EvalStandardBE.Delete(obj);

            return this.OkResult();
        }
    }
}
