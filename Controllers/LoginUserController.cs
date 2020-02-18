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
    [RoutePrefix("LoginUser")]
    public class LoginUserController : BaseController
    {
        private readonly ILoginUserBE LoginUserBE;
        public LoginUserController(ILoginUserBE _LoginUserBE,
                               IMapper mapper) : base(mapper)
        {
            LoginUserBE = _LoginUserBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await LoginUserBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] LoginUserGetByIdReq req)
        {
            var obj = LoginUserBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.LoginUserNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(LoginUserInsertReq req)
        {
            var existobj = await LoginUserBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.LoginUserHasExist));
            }

            LoginUserBE.Insert(Mapper.Map<LoginUser>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(LoginUserUpdateReq req)
        {
            var obj = await LoginUserBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.LoginUserNotExist));
            }

            Mapper.Map(req, obj);

            LoginUserBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(LoginUserDeleteReq req)
        {
            var obj = await LoginUserBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.LoginUserNotExist));
            }

            LoginUserBE.Delete(obj);

            return this.OkResult();
        }
    }
}
