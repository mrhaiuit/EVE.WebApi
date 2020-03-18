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
    [RoutePrefix("auth")]
    public class ReportController : BaseController
    {
        public readonly ILoginBE _loginBE;
        public ReportController(ILoginBE loginBE,
                               IMapper mapper) : base(mapper)
        {
            _loginBE = loginBE;
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
