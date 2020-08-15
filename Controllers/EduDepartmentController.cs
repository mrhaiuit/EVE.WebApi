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
    [RoutePrefix("EduDepartment")]
    public class EduDepartmentController : BaseController
    {
        private readonly IEduDepartmentBE EduDepartmentBE;
        public EduDepartmentController(IEduDepartmentBE _EduDepartmentBE,
                               IMapper mapper) : base(mapper)
        {
            EduDepartmentBE = _EduDepartmentBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EduDepartmentBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }


        [Route("GetByUserGroupEmployee")]
        public async Task<HttpResponseMessage> GetByUserGroupEmployee([FromUri]UserGroupEmployeeReq req)
        {
            var objs = await EduDepartmentBE.GetByUserGroupEmployee(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("SearchByName")]
        public async Task<HttpResponseMessage> SearchByName([FromUri]EduDepartmentSearchByNameReq req)
        {
            var objs = await EduDepartmentBE.SearchByName(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("GetByEduProvinceId")]
        public async Task<HttpResponseMessage> GetByEduProvinceId([FromUri]EduProvinceBaseReq req)
        {
            var objs = await EduDepartmentBE.GetByEduProvinceId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EduDepartmentGetByIdReq req)
        {
            var obj =await EduDepartmentBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EduDepartmentNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EduDepartmentInsertReq req)
        {
            var existobj = await EduDepartmentBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EduDepartmentHasExist));
            }

            if (EduDepartmentBE.Insert(Mapper.Map<EduDepartment>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EduDepartmentUpdateReq req)
        {
            var obj = await EduDepartmentBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduDepartmentNotExist));
            }

            Mapper.Map(req, obj);
            if (EduDepartmentBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EduDepartmentDeleteReq req)
        {
            var obj = await EduDepartmentBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduDepartmentNotExist));
            }

            if (EduDepartmentBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
