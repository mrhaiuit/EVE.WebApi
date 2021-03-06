﻿using System;
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
    [RoutePrefix("UserGroup")]
    public class UserGroupController : BaseController
    {
        private readonly IUserGroupBE UserGroupBE;
        public UserGroupController(IUserGroupBE _UserGroupBE,
                               IMapper mapper) : base(mapper)
        {
            UserGroupBE = _UserGroupBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await UserGroupBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] UserGroupGetByIdReq req)
        {
            var obj = await UserGroupBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UserGroupNotExist));
        }
        // Task<List<UserGroup>> GetByUserGroup(UserGroupBaseReq userGroup)

        [Route("GetByUserGroup")]
        public async Task<HttpResponseMessage> GetByUserGroup([FromUri] UserGroupBaseReq userGroup)
        {
            var obj = await UserGroupBE.GetByUserGroup(userGroup);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UserGroupNotExist));
        }

        [Route("GetFormsByUserGroup")]
        public async Task<HttpResponseMessage> GetFormsByUserGroup([FromUri] UserGroupBaseReq userGroup)
        {
            var obj = await UserGroupBE.GetFormsByUserGroup(userGroup);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UserGroupNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(UserGroupInsertReq req)
        {
            var existobj = await UserGroupBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupHasExist));
            }
            if (UserGroupBE.Insert(Mapper.Map<UserGroup>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(UserGroupUpdateReq req)
        {
            var obj = await UserGroupBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupNotExist));
            }

            Mapper.Map(req, obj);
            if (UserGroupBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(UserGroupDeleteReq req)
        {
            var obj = await UserGroupBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupNotExist));
            }

            if (UserGroupBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
