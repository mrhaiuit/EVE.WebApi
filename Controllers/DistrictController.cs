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
    [RoutePrefix("District")]
    public class DistrictController : BaseController
    {
        private readonly IDistrictBE DistrictBE;
        public DistrictController(IDistrictBE _DistrictBE,
                               IMapper mapper) : base(mapper)
        {
            DistrictBE = _DistrictBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await DistrictBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("GetByProvinceId")]
        public async Task<HttpResponseMessage> GetByProvinceId([FromUri]ProvinceBaseReq req)
        {
            var objs = await DistrictBE.GetByProvinceId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] DistrictBaseReq req)
        {
            var obj =await DistrictBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.DistrictNotExist));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(DistrictBaseReq req)
        {
            var obj = await DistrictBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.DistrictNotExist));
            }

            DistrictBE.Delete(obj);

            return this.OkResult();
        }
    }
}
