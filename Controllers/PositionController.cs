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
    [RoutePrefix("Position")]
    public class PositionController : BaseController
    {
        private readonly IPositionBE PositionBE;
        public PositionController(IPositionBE _PositionBE,
                               IMapper mapper) : base(mapper)
        {
            PositionBE = _PositionBE;
        }

        [Route("all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var objs = await PositionBE.GetAllAsync();
            if (objs != null
               && objs.Any())
            {
                return this.OkResult(objs.ToList()
                                           .RemoveWhiteSpaceForList());
            }

            return this.OkResult();
        }

        [Route("getById")]
        public async Task<HttpResponseMessage> GetById([FromUri] PositionBaseReq req)
        {
            var obj =await PositionBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.PositionNotExist));
        }

        [Route("GetByEduLevel")]
        public async Task<HttpResponseMessage> GetByEduLevel([FromUri] PositionByEduLevelReq req)
        {
            var obj =await PositionBE.GetByEduLevel(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.PositionNotExist));
        }
    }
}
