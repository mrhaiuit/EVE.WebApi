﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using EVE.ApiModels.Catalog;
using EVE.Bussiness;
using EVE.Commons;
using EVE.WebApi.Shared;
using EVE.WebApi.Shared.Response;

namespace EVE.WebApi.Controllers
{
    [RoutePrefix("Country")]
    public class CountryController : BaseController
    {
        private readonly ICountryBE CountryBE;
        public CountryController(ICountryBE _CountryBE,
                               IMapper mapper) : base(mapper)
        {
            CountryBE = _CountryBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await CountryBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] CountryBaseReq req)
        {
            var obj =await CountryBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.CountryNotExist));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(CountryBaseReq req)
        {
            var obj = await CountryBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.CountryNotExist));
            }

            if (CountryBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
