using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using EVE.ApiModels.Authentication.Request;
using EVE.Bussiness;
using EVE.Commons;
using EVE.Data;
using EVE.WebApi.Authentication.Helper;
using EVE.WebApi.Shared;
using EVE.WebApi.Shared.Response;

namespace EVE.WebApi.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeBE employeeBE;
        public EmployeeController(IEmployeeBE _employeeBE,
                               IMapper mapper) : base(mapper)
        {
            employeeBE = _employeeBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await employeeBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] EmployeeGetByIdReq req)
        {
            var obj = employeeBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.EmployeeNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EmployeeInsertReq req)
        {
            var existobj = await employeeBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EmployeeHasExist));
            }

            employeeBE.Insert(Mapper.Map<Employee>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EmployeeUpdateReq req)
        {
            var obj = await employeeBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EmployeeNotExist));
            }

            Mapper.Map(req, obj);

            employeeBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EmployeeDeleteReq req)
        {
            var obj = await employeeBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EmployeeNotExist));
            }

            employeeBE.Delete(obj);

            return this.OkResult();
        }
    }
}
