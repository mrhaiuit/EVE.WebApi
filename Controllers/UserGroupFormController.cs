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
    [RoutePrefix("UserGroupForm")]
    public class UserGroupFormController : BaseController
    {
        private readonly IUserGroupFormBE UserGroupFormBE;
        public UserGroupFormController(IUserGroupFormBE _UserGroupFormBE,
                               IMapper mapper) : base(mapper)
        {
            UserGroupFormBE = _UserGroupFormBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await UserGroupFormBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] UserGroupFormGetByIdReq req)
        {
            var obj = UserGroupFormBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.UserGroupFormNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(UserGroupFormInsertReq req)
        {
            var existobj = await UserGroupFormBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupFormHasExist));
            }

            UserGroupFormBE.Insert(Mapper.Map<UserGroup_Form>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(UserGroupFormUpdateReq req)
        {
            var obj = await UserGroupFormBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupFormNotExist));
            }

            Mapper.Map(req, obj);

            UserGroupFormBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(UserGroupFormDeleteReq req)
        {
            var obj = await UserGroupFormBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupFormNotExist));
            }

            UserGroupFormBE.Delete(obj);

            return this.OkResult();
        }
    }
}
