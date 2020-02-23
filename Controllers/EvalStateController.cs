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
    [RoutePrefix("EvalState")]
    public class EvalStateController : BaseController
    {
        private readonly IEvalStateBE EvalStateBE;
        public EvalStateController(IEvalStateBE _EvalStateBE,
                               IMapper mapper) : base(mapper)
        {
            EvalStateBE = _EvalStateBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalStateBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalStateGetByIdReq req)
        {
            var obj =await EvalStateBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalStateNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalStateInsertReq req)
        {
            var existobj = await EvalStateBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStateHasExist));
            }
            if (EvalStateBE.Insert(Mapper.Map<EvalState>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalStateUpdateReq req)
        {
            var obj = await EvalStateBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStateNotExist));
            }

            Mapper.Map(req, obj);
            if (EvalStateBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalStateDeleteReq req)
        {
            var obj = await EvalStateBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalStateNotExist));
            }
            if (EvalStateBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
