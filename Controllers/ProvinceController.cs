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
    [RoutePrefix("Province")]
    public class ProvinceController : BaseController
    {
        private readonly IProvinceBE ProvinceBE;
        public ProvinceController(IProvinceBE _ProvinceBE,
                               IMapper mapper) : base(mapper)
        {
            ProvinceBE = _ProvinceBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await ProvinceBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] ProvinceBaseReq req)
        {
            var obj = ProvinceBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.ProvinceNotExist));
        }


        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(ProvinceBaseReq req)
        {
            var obj = await ProvinceBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.ProvinceNotExist));
            }

            ProvinceBE.Delete(obj);

            return this.OkResult();
        }
    }
}
