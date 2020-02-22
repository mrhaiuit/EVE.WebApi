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
    [RoutePrefix("EvalPeriod")]
    public class EvalPeriodController : BaseController
    {
        private readonly IEvalPeriodBE EvalPeriodBE;
        public EvalPeriodController(IEvalPeriodBE _EvalPeriodBE,
                               IMapper mapper) : base(mapper)
        {
            EvalPeriodBE = _EvalPeriodBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalPeriodBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }
        [Route("GetByYearAndSchool")]
        public async Task<HttpResponseMessage> GetByYearAndSchool([FromUri] EvalPeriodGetByYearAndSchoolReq req)
        {
            var obj =  EvalPeriodBE.GetByYearAndSchool(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalPeriodNotExist));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalPeriodGetByIdReq req)
        {
            var obj = await EvalPeriodBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalPeriodNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalPeriodInsertReq req)
        {
            var existobj = await EvalPeriodBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalPeriodHasExist));
            }

            EvalPeriodBE.Insert(Mapper.Map<EvalPeriod>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalPeriodUpdateReq req)
        {
            var obj = await EvalPeriodBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalPeriodNotExist));
            }

            Mapper.Map(req, obj);

            EvalPeriodBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalPeriodDeleteReq req)
        {
            var obj = await EvalPeriodBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalPeriodNotExist));
            }

            EvalPeriodBE.Delete(obj);

            return this.OkResult();
        }
    }
}
