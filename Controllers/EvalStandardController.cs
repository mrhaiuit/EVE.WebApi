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
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }


        [Route("GetByLevelAndType")]
        public async Task<HttpResponseMessage> GetByLevelAndType([FromUri] EvalStandardGetByLevelAndTypeReq req)
        {
            var obj = await EvalStandardBE.GetByLevelAndType(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("GetEvalCriteriaByStandard")]
        public async Task< HttpResponseMessage> GetEvalCriteriaByStandard([FromUri] int StandardId)
        {
            var obj =await EvalStandardBE.GetEvalCriteriaByStandard(StandardId);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.CateriaNotExistWithStandard));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalStandardGetByIdReq req)
        {
            var obj =await EvalStandardBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
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
            if (EvalStandardBE.Insert(Mapper.Map<EvalStandard>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (EvalStandardBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalStandardDeleteReq req)
        {
            var obj = await EvalStandardBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStandardNotExist));
            }
            if (EvalStandardBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
