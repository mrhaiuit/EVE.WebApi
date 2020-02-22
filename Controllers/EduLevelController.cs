using System;
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
    [RoutePrefix("EduLevel")]
    public class EduLevelController : BaseController
    {
        private readonly IEduLevelBE EduLevelBE;
        public EduLevelController(IEduLevelBE _EduLevelBE,
                               IMapper mapper) : base(mapper)
        {
            EduLevelBE = _EduLevelBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await EduLevelBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] EduLevelBaseReq req)
        {
            var obj =await EduLevelBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EduLevelNotExist));
        }

        [Route("GetByUsergroup")]
        public async Task<HttpResponseMessage> GetByUsergroup([FromUri] UserGroupBaseReq req)
        {
            var obj = await EduLevelBE.GetByUserGroup(req);
            if (obj != null)
            {
                return this.OkResult(obj);
            }

            return this.ErrorResult(new Error(EnumError.EduLevelNotExist));
        }



        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(EduLevelBaseReq req)
        {
            var obj = await EduLevelBE.GetById(req);
            if (obj == null)
            {
                return this.ErrorResult(new Error(EnumError.EduLevelNotExist));
            }

            EduLevelBE.Delete(obj);

            return this.OkResult();
        }
    }
}
