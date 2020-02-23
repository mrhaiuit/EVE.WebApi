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
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }


        [Route("GetByUserGroupEmployee")]
        public async Task<HttpResponseMessage> GetByUserGroupEmployee([FromUri]UserGroupEmployeeReq req)
        {
            var objs = await EduProvinceBE.GetByUserGroupEmployee(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EduProvinceGetByIdReq req)
        {
            var obj =await EduProvinceBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
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
            if (EduProvinceBE.Insert(Mapper.Map<EduProvince>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (EduProvinceBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EduProvinceDeleteReq req)
        {
            var obj = await EduProvinceBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduProvinceNotExist));
            }


            if (EduProvinceBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
