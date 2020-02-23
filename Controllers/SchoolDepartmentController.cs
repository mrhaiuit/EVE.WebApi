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
    [RoutePrefix("SchoolDepartment")]
    public class SchoolDepartmentController : BaseController
    {
        private readonly ISchoolDepartmentBE SchoolDepartmentBE;
        public SchoolDepartmentController(ISchoolDepartmentBE _SchoolDepartmentBE,
                               IMapper mapper) : base(mapper)
        {
            SchoolDepartmentBE = _SchoolDepartmentBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SchoolDepartmentBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }


        [Route("GetBySchoolId")]
        public async Task<HttpResponseMessage> GetBySchoolId([FromUri]SchoolBaseReq req)
        {
            var objs = await SchoolDepartmentBE.GetBySchoolId(req);
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs);
            }

            return this.ErrorResult(new Error(EnumError.DataNotFound));
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] SchoolDepartmentGetByIdReq req)
        {
            var obj =await SchoolDepartmentBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.SchoolDepartmentNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(SchoolDepartmentInsertReq req)
        {
            var existobj = await SchoolDepartmentBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolDepartmentHasExist));
            }

            if (SchoolDepartmentBE.Insert(Mapper.Map<SchoolDepartment>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));

        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(SchoolDepartmentUpdateReq req)
        {
            var obj = await SchoolDepartmentBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolDepartmentNotExist));
            }

            Mapper.Map(req, obj);
            if (SchoolDepartmentBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SchoolDepartmentDeleteReq req)
        {
            var obj = await SchoolDepartmentBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolDepartmentNotExist));
            }

            if (SchoolDepartmentBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
