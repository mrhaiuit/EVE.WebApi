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
    [RoutePrefix("SchoolLevel")]
    public class SchoolLevelController : BaseController
    {
        private readonly ISchoolLevelBE SchoolLevelBE;
        private readonly IEduLevelBE EduLevelBE;
        public SchoolLevelController(ISchoolLevelBE _SchoolLevelBE,
            IEduLevelBE eduLevelBE,
            IMapper mapper) : base(mapper)
        {
            SchoolLevelBE = _SchoolLevelBE;
            EduLevelBE = eduLevelBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await SchoolLevelBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] SchoolLevelGetByIdReq req)
        {
            var obj =await SchoolLevelBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
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
            if (SchoolLevelBE.Insert(Mapper.Map<SchoolLevel>(req)))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.InsertFailse));
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
            if (SchoolLevelBE.Update(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.UpdateFailse));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(SchoolLevelDeleteReq req)
        {
            var obj = await SchoolLevelBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.SchoolLevelNotExist));
            }
            if (SchoolLevelBE.Delete(obj))
                return this.OkResult();
            else
                return this.ErrorResult(new Error(EnumError.DeleteFailse));
        }
    }
}
