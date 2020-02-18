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
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getEvalDetailByMasterId")]
        public HttpResponseMessage GetEvalDetailByMasterId([FromUri] int masterId)
        {
            var obj = EvalMasterBE.GetEvalDetailByMasterId(masterId);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EvalMasterGetByIdReq req)
        {
            var obj = EvalMasterBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
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

            EvalMasterBE.Insert(Mapper.Map<EvalMaster>(req));
            return this.OkResult();
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

            EvalMasterBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalMasterDeleteReq req)
        {
            var obj = await EvalMasterBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalMasterNotExist));
            }

            EvalMasterBE.Delete(obj);

            return this.OkResult();
        }
    }
}
