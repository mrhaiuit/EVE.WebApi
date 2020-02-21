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
    [RoutePrefix("Ward")]
    public class WardController : BaseController
    {
        private readonly IWardBE WardBE;
        public WardController(IWardBE _WardBE,
                               IMapper mapper) : base(mapper)
        {
            WardBE = _WardBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await WardBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }
        
        [Route("GetByDistrictId")]
        public async Task<HttpResponseMessage> GetByDistrictId([FromUri]DistrictBaseReq req)
        {
            var objs = await WardBE.GetByDistrictId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("GetByProvinceId")]
        public async Task<HttpResponseMessage> GetByProvinceId([FromUri]ProvinceBaseReq req)
        {
            var objs = await WardBE.GetByProvinceId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] WardBaseReq req)
        {
            var obj =await WardBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.WardNotExist));
        }

    }
}
