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
    [RoutePrefix("EvalResult")]
    public class EvalResultController : BaseController
    {
        private readonly IEvalResultBE EvalResultBE;
        public EvalResultController(IEvalResultBE _EvalResultBE,
                               IMapper mapper) : base(mapper)
        {
            EvalResultBE = _EvalResultBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EvalResultBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EvalResultGetByIdReq req)
        {
            var obj =await EvalResultBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(EvalResultInsertReq req)
        {
            var existobj = await EvalResultBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultHasExist));
            }
            if (EvalResultBE.Insert(Mapper.Map<EvalResult>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(EvalResultUpdateReq req)
        {
            var obj = await EvalResultBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
            }

            Mapper.Map(req, obj);

            EvalResultBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EvalResultDeleteReq req)
        {
            var obj = await EvalResultBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EvalResultNotExist));
            }

            if (EvalResultBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
