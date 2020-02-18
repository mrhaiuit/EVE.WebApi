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
    [RoutePrefix("School")]
    public class SchoolController : BaseController
    {
        private readonly ISchoolBE SchoolBE;
        public SchoolController(ISchoolBE _SchoolBE,
                               IMapper mapper) : base(mapper)
        {
            SchoolBE = _SchoolBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SchoolBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] SchoolGetByIdReq req)
        {
            var obj = SchoolBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.SchoolNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(SchoolInsertReq req)
        {
            var existobj = await SchoolBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolHasExist));
            }

            SchoolBE.Insert(Mapper.Map<School>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(SchoolUpdateReq req)
        {
            var obj = await SchoolBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolNotExist));
            }

            Mapper.Map(req, obj);

            SchoolBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SchoolDeleteReq req)
        {
            var obj = await SchoolBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolNotExist));
            }

            SchoolBE.Delete(obj);

            return this.OkResult();
        }
    }
}
