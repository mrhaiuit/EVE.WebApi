using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;

using EVE.Bussiness;
using EVE.Commons;
using EVE.Data;
using EVE.WebApi.Shared;
using EVE.WebApi.Shared.Response;

namespace EVE.WebApi.Controllers
{
    [RoutePrefix("auth")]
    public class ReportController : BaseController
    {
        public readonly ILoginBE _loginBE;
        public ReportController(ILoginBE loginBE,
                               IMapper mapper) : base(mapper)
        {
            _loginBE = loginBE;
        }

        [HttpPost]
        [Route("logon")]
        public async Task<HttpResponseMessage> Logon(LoginReq req)
        {
            var employee = await _loginBE.GetEmployeeByAccount(req);
            if (employee != null)
            {
                var curTime = DateTime.Now;
                var logonUser = new LoginUser
                {
                    UserName = employee.UserName,
                    UserId = employee.EmployeeCode,
                    IpAddress = AppUtil.GetClientIp(Request),
                    LoginDate = curTime,
                    UpdTs = curTime
                };

               await _loginBE.SaveLogin(logonUser);

                return this.OkResult(logonUser);
            }

            return this.ErrorResult(new Error(EnumError.LogonInvalid));
        }


        [HttpGet]
        [Route("GetUserGroupByUserName")]
        public async Task<HttpResponseMessage> GetUserGroupByUserName([FromUri]UserNameReq req)
        {
            try
            {
                var obj = await _loginBE.GetUserGroupByUserName(req);
                if (obj == null || !obj.Any())
                {
                    return this.ErrorResult(new Error(EnumError.UserNotGrandPermission));
                }
                return this.OkResult(obj);
            }
            catch(Exception ex)
            {
                return this.ErrorResult(new Error("", ex.Message));
            }
        }
    }
}
