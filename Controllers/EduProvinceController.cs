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
    [RoutePrefix("EduProvince")]
    public class EduProvinceController : BaseController
    {
        private readonly IEduProvinceBE EduProvinceBE;
        public EduProvinceController(IEduProvinceBE _EduProvinceBE,
                               IMapper mapper) : base(mapper)
        {
            EduProvinceBE = _EduProvinceBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EduProvinceBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EduProvinceGetByIdReq req)
        {
            var obj = EduProvinceBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EduProvinceNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EduProvinceInsertReq req)
        {
            var existobj = await EduProvinceBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EduProvinceHasExist));
            }

            EduProvinceBE.Insert(Mapper.Map<EduProvince>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EduProvinceUpdateReq req)
        {
            var obj = await EduProvinceBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduProvinceNotExist));
            }

            Mapper.Map(req, obj);

            EduProvinceBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EduProvinceDeleteReq req)
        {
            var obj = await EduProvinceBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduProvinceNotExist));
            }

            EduProvinceBE.Delete(obj);

            return this.OkResult();
        }
    }
}
