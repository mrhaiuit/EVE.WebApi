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
    [RoutePrefix("EvalMaster")]
    public class EvalMasterController : BaseController
    {
        private readonly IEvalDetailBE EvalDetailBE;
        private readonly IEvalMasterBE EvalMasterBE;
        public EvalMasterController(IEvalDetailBE evalDetailBE,
                                    IEvalMasterBE evalMasterBE,
                                    IMapper mapper) : base(mapper)
        {
            EvalDetailBE = evalDetailBE;
            EvalMasterBE = evalMasterBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalMasterBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("CompleteFinal")]
        public async Task<HttpResponseMessage> CompleteFinal([FromBody] EvalMasterBaseReq req)
        {
            var evalMaster = await EvalMasterBE.GetById(req);
            if (evalMaster == null)
                return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));

            var objExits = (await EvalMasterBE.GetAsync(p => p.EvalPeriodId == evalMaster.EvalPeriodId))?.FirstOrDefault();
            if(objExits!=null && objExits.EvalMasterId == req.EvalMasterId)
            {
                return this.ErrorResult(new Error(EnumError.EvalMasterFinalHasExits));
            }
            var obj = await EvalMasterBE.CompleteFinal(req);
            if (obj)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UpdateFaile));
        }

        [Route("CancelFinal")]
        public async Task<HttpResponseMessage> CancelFinal([FromBody] EvalMasterBaseReq req)
        {
            var evalMaster = await EvalMasterBE.GetById(req);
            if (evalMaster == null)
                return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));

            var obj = await EvalMasterBE.CancelFinal(req);
            if (obj)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UpdateFaile));
        }

        [Route("GetEvalResultSumary")]
        public async Task<HttpResponseMessage> GetEvalResultSumary([FromUri] ExeEvalDetailByMasterIdReq req)
        {
            var obj = await EvalMasterBE.GetEvalResultSumary(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
        }

        [Route("GetEvalByUserId")]
        public async Task<HttpResponseMessage> GetEvalByUserId([FromUri] EvalMasterGetByUserIdReq req)
        {
            var obj = await EvalMasterBE.GetEvalByUserId(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
        }

        [Route("GetSelfEvalByUserId")]
        public async Task<HttpResponseMessage> GetSelfEvalByUserId([FromUri] EvalMasterGetByUserIdReq req)
        {
            var obj = await EvalMasterBE.GetSelfEvalByUserId(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
        }
        // Task<List<EvalMaster>> GetEvalByUserId(EvalMasterGetByUserIdReq req)

        [Route("ExeEvalDetailByMasterId")]
        public async Task<HttpResponseMessage> ExeEvalDetailByMasterId([FromBody] ExeEvalDetailByMasterIdReq req)
        {
            var obj = await EvalMasterBE.ExeEvalDetailByMasterId(req);
            var obj1 = obj.Select(p => new { p.EvalStandardName, p.EvalStandardId }).Distinct();
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
        }

        [Route("getEvalDetailByMasterId")]
        public async Task<HttpResponseMessage> GetEvalDetailByMasterId([FromUri] EvalMasterBaseReq req)
        {
            var obj =await EvalMasterBE.GetEvalDetailByMasterId(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalMasterGetByIdReq req)
        {
            var obj =await EvalMasterBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalMasterInsertReq req)
        {
            var existobj = await EvalMasterBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalMasterHasExist));
            }
            if (EvalMasterBE.Insert(Mapper.Map<EvalMaster>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalMasterUpdateReq req)
        {
            var obj = await EvalMasterBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
            }

            Mapper.Map(req, obj);
            if (EvalMasterBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalMasterDeleteReq req)
        {
            var obj = await EvalMasterBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
            }
            if (EvalMasterBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
