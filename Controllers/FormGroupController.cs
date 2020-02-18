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
    [RoutePrefix("FormGroup")]
    public class FormGroupController : BaseController
    {
        private readonly IFormGroupBE FormGroupBE;
        public FormGroupController(IFormGroupBE _FormGroupBE,
                               IMapper mapper) : base(mapper)
        {
            FormGroupBE = _FormGroupBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await FormGroupBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] FormGroupGetByIdReq req)
        {
            var obj = FormGroupBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.FormGroupNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(FormGroupInsertReq req)
        {
            var existobj = await FormGroupBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.FormGroupHasExist));
            }

            FormGroupBE.Insert(Mapper.Map<FormGroup>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(FormGroupUpdateReq req)
        {
            var obj = await FormGroupBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.FormGroupNotExist));
            }

            Mapper.Map(req, obj);

            FormGroupBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(FormGroupDeleteReq req)
        {
            var obj = await FormGroupBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.FormGroupNotExist));
            }

            FormGroupBE.Delete(obj);

            return this.OkResult();
        }
    }
}
