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
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] LoginUserGetByIdReq req)
        {
            var obj =await LoginUserBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
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
            if (LoginUserBE.Insert(Mapper.Map<LoginUser>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (LoginUserBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(LoginUserDeleteReq req)
        {
            var obj = await LoginUserBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.LoginUserNotExist));
            }

            if (LoginUserBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
