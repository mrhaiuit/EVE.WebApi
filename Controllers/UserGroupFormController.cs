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
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] UserGroupFormGetByIdReq req)
        {
            var obj =await UserGroupFormBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
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
            if (UserGroupFormBE.Insert(Mapper.Map<UserGroup_Form>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (UserGroupFormBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(UserGroupFormDeleteReq req)
        {
            var obj = await UserGroupFormBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupFormNotExist));
            }
            if (UserGroupFormBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
