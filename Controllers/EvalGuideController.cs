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
    [RoutePrefix("EvalGuide")]
    public class EvalGuideController : BaseController
    {
        private readonly IEvalGuideBE EvalGuideBE;
        public EvalGuideController(IEvalGuideBE _EvalGuideBE,
                               IMapper mapper) : base(mapper)
        {
            EvalGuideBE = _EvalGuideBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalGuideBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EvalGuideGetByIdReq req)
        {
            var obj = EvalGuideBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EvalGuideNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalGuideInsertReq req)
        {
            var existobj = await EvalGuideBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalGuideHasExist));
            }

            EvalGuideBE.Insert(Mapper.Map<EvalGuide>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalGuideUpdateReq req)
        {
            var obj = await EvalGuideBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalGuideNotExist));
            }

            Mapper.Map(req, obj);

            EvalGuideBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalGuideDeleteReq req)
        {
            var obj = await EvalGuideBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalGuideNotExist));
            }

            EvalGuideBE.Delete(obj);

            return this.OkResult();
        }
    }
}
