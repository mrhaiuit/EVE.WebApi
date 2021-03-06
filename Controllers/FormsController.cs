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
    [RoutePrefix("Forms")]
    public class FormsController : BaseController
    {
        private readonly IFormsBE FormsBE;
        public FormsController(IFormsBE _FormsBE,
                               IMapper mapper) : base(mapper)
        {
            FormsBE = _FormsBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await FormsBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] FormsGetByIdReq req)
        {
            var obj =await FormsBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.FormsNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(FormsInsertReq req)
        {
            var existobj = await FormsBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.FormsHasExist));
            }
            if (FormsBE.Insert(Mapper.Map<Form>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(FormsUpdateReq req)
        {
            var obj = await FormsBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.FormsNotExist));
            }

            Mapper.Map(req, obj);

            if (FormsBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(FormsDeleteReq req)
        {
            var obj = await FormsBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.FormsNotExist));
            }

            if (FormsBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
