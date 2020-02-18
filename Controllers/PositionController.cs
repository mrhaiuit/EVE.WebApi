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
        public HttpResponseMessage GetById([FromUri] PositionBaseReq req)
        {
            var obj = PositionBE.GetById(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.PositionNotExist));
        }

        [Route("GetByEduLevel")]
        public HttpResponseMessage GetByEduLevel([FromUri] PositionByEduLevelReq req)
        {
            var obj = PositionBE.GetByEduLevel(req);
            if (obj != null)
            {
                return this.OkResult(obj.RemoveWhiteSpace());
            }

            return this.ErrorResult(new Error(EnumError.PositionNotExist));
        }
    }
}
