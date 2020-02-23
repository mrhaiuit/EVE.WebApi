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
    [RoutePrefix("School")]
    public class SchoolController : BaseController
    {
        private readonly ISchoolBE SchoolBE;
        public SchoolController(ISchoolBE _SchoolBE,
                               IMapper mapper) : base(mapper)
        {
            SchoolBE = _SchoolBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SchoolBE.GetAllAsync();
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
            var objs = await SchoolBE.GetByUserGroupEmployee(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("GetByEduDepartmentId")]
        public async Task<HttpResponseMessage> GetByEduDepartmentId([FromUri]EduDepartmentBaseReq req)
        {
            var objs = await SchoolBE.GetByEduDepartmentId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("GetByEduProvinceId")]
        public async Task<HttpResponseMessage> GetByEduProvinceId([FromUri]EduProvinceBaseReq req)
        {
            var objs = await SchoolBE.GetByEduProvinceId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] SchoolGetByIdReq req)
        {
            var obj = await SchoolBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.SchoolNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(SchoolInsertReq req)
        {
            var existobj = await SchoolBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolHasExist));
            }
            if (SchoolBE.Insert(Mapper.Map<School>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(SchoolUpdateReq req)
        {
            var obj = await SchoolBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolNotExist));
            }

            Mapper.Map(req, obj);
            if (SchoolBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SchoolDeleteReq req)
        {
            var obj = await SchoolBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolNotExist));
            }
            if (SchoolBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
