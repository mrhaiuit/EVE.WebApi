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
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EmployeeGetByIdReq req)
        {
            var obj =await employeeBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EmployeeNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EmployeeInsertReq req)
        {
            var employee = await employeeBE.GetByUserName(new UserNameReq() { UserName = req.UserName });
            if (employee != null)
                return this.ErrorResult(new Error(EnumError.UserNameHasExits));
            var existobj = await employeeBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EmployeeHasExist));
            }
            if (employeeBE.Insert(Mapper.Map<Employee>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (employeeBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EmployeeDeleteReq req)
        {
            var obj = await employeeBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EmployeeNotExist));
            }

            if (employeeBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
