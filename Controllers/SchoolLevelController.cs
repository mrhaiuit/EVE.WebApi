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
    [RoutePrefix("SchoolLevel")]
    public class SchoolLevelController : BaseController
    {
        private readonly ISchoolLevelBE SchoolLevelBE;
        public SchoolLevelController(ISchoolLevelBE _SchoolLevelBE,
                               IMapper mapper) : base(mapper)
        {
            SchoolLevelBE = _SchoolLevelBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SchoolLevelBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public HttpResponseMessage GetById([FromUri] SchoolLevelGetByIdReq req)
        {
            var obj = SchoolLevelBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.SchoolLevelNotExist));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Insert(SchoolLevelInsertReq req)
        {
            var existobj = await SchoolLevelBE.GetById(req);
            if (existobj != null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolLevelHasExist));
            }

            SchoolLevelBE.Insert(Mapper.Map<SchoolLevel>(req));
            return this.OkResult();
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update(SchoolLevelUpdateReq req)
        {
            var obj = await SchoolLevelBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolLevelNotExist));
            }

            Mapper.Map(req, obj);

            SchoolLevelBE.Update(obj);

            return this.OkResult();
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SchoolLevelDeleteReq req)
        {
            var obj = await SchoolLevelBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolLevelNotExist));
            }

            SchoolLevelBE.Delete(obj);

            return this.OkResult();
        }
    }
}
