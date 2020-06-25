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
    [RoutePrefix("SubPrincipalCriteria")]
    public class SubPrincipalCriteriaController : BaseController
    {
        private readonly ISubPrincipalCriteriaBE SubPrincipalCriteriaBE;
        public SubPrincipalCriteriaController(ISubPrincipalCriteriaBE _SubPrincipalCriteriaBE,
                               IMapper mapper) : base(mapper)
        {
            SubPrincipalCriteriaBE = _SubPrincipalCriteriaBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SubPrincipalCriteriaBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] SubPrincipalCriteriaBaseReq req)
        {
            var obj = await SubPrincipalCriteriaBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.SubPrincipalCriteriaNotExits));
        }

        [Route("GetByEmployeeAndPeriod")]
        public async Task<HttpResponseMessage> GetByEmployeeAndPeriod([FromUri] GetByEmployeeAndPeriodReq req)
        {
            var obj = await SubPrincipalCriteriaBE.GetByEmployeeAndPeriod(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.SubPrincipalCriteriaNotExits));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(SubPrincipalCriteriaInsertReq req)
        {
            var existobj = await SubPrincipalCriteriaBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.SubPrincipalCriteriaHasExist));
            }
            if (SubPrincipalCriteriaBE.Insert(Mapper.Map<SubPrincipalCriteria>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(SubPrincipalCriteriaUpdateReq req)
        {
            var obj = await SubPrincipalCriteriaBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SubPrincipalCriteriaNotExits));
            }

            Mapper.Map(req, obj);
            if (SubPrincipalCriteriaBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SubPrincipalCriteriaDeleteReq req)
        {
            var obj = await SubPrincipalCriteriaBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SubPrincipalCriteriaNotExits));
            }
            if (SubPrincipalCriteriaBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
