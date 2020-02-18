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
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EduDepartmentGetByIdReq req)
        {
            var obj = EduDepartmentBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
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

            EduDepartmentBE.Insert(Mapper.Map<EduDepartment>(req));
            return this.OkResult();
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

            EduDepartmentBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EduDepartmentDeleteReq req)
        {
            var obj = await EduDepartmentBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduDepartmentNotExist));
            }

            EduDepartmentBE.Delete(obj);

            return this.OkResult();
        }
    }
}
