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
    [RoutePrefix("EvalDetail")]
    public class EvalDetailController : BaseController
    {
        private readonly IEvalDetailBE EvalDetailBE;
        public EvalDetailController(IEvalDetailBE _EvalDetailBE,
                               IMapper mapper) : base(mapper)
        {
            EvalDetailBE = _EvalDetailBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalDetailBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalDetailGetByIdReq req)
        {
            var obj =await EvalDetailBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalDetailInsertReq req)
        {
            var existobj = await EvalDetailBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalDetailHasExist));
            }

            EvalDetailBE.Insert(Mapper.Map<EvalDetail>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalDetailUpdateReq req)
        {
            var obj = await EvalDetailBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
            }

            Mapper.Map(req, obj);

            EvalDetailBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalDetailDeleteReq req)
        {
            var obj = await EvalDetailBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalDetailNotExist));
            }

            EvalDetailBE.Delete(obj);

            return this.OkResult();
        }
    }
}
