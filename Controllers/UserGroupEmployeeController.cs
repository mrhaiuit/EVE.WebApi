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
    [RoutePrefix("UserGroupEmployee")]
    public class UserGroupEmployeeController : BaseController
    {
        private readonly IUserGroupEmployeeBE UserGroupEmployeeBE;
        public UserGroupEmployeeController(IUserGroupEmployeeBE _UserGroupEmployeeBE,
                               IMapper mapper) : base(mapper)
        {
            UserGroupEmployeeBE = _UserGroupEmployeeBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await UserGroupEmployeeBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] UserGroupEmployeeGetByIdReq req)
        {
            var obj =await UserGroupEmployeeBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.UserGroupEmployeeNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(UserGroupEmployeeInsertReq req)
        {
            var existobj = await UserGroupEmployeeBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupEmployeeHasExist));
            }
            if (UserGroupEmployeeBE.Insert(Mapper.Map<UserGroup_Employee>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(UserGroupEmployeeUpdateReq req)
        {
            var obj = await UserGroupEmployeeBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupEmployeeNotExist));
            }

            Mapper.Map(req, obj);
            if (UserGroupEmployeeBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(UserGroupEmployeeDeleteReq req)
        {
            var obj = await UserGroupEmployeeBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.UserGroupEmployeeNotExist));
            }
            if (UserGroupEmployeeBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
