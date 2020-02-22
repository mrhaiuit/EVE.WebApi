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
    [RoutePrefix("EvalCriteria")]
    public class EvalCriteriaController : BaseController
    {
        private readonly IEvalCriteriaBE EvalCriteriaBE;
        public EvalCriteriaController(IEvalCriteriaBE _EvalCriteriaBE,
                               IMapper mapper) : base(mapper)
        {
            EvalCriteriaBE = _EvalCriteriaBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalCriteriaBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("GetByStandardId")]
        public async Task<HttpResponseMessage> GetByStandardId([FromUri] EvalStandardBaseReq req)
        {
            var obj = await EvalCriteriaBE.GetByStandardId(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalCriteriaNotExist));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalCriteriaGetByIdReq req)
        {
            var obj = await EvalCriteriaBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalCriteriaNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalCriteriaInsertReq req)
        {
            var existobj = await EvalCriteriaBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalCriteriaHasExist));
            }

            EvalCriteriaBE.Insert(Mapper.Map<EvalCriteria>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalCriteriaUpdateReq req)
        {
            var obj = await EvalCriteriaBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalCriteriaNotExist));
            }

            Mapper.Map(req, obj);

            EvalCriteriaBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalCriteriaDeleteReq req)
        {
            var obj = await EvalCriteriaBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalCriteriaNotExist));
            }

            EvalCriteriaBE.Delete(obj);

            return this.OkResult();
        }
    }
}
