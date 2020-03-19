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
    [RoutePrefix("report")]
    public class ReportController : BaseController
    {
        public readonly IReportBE ReportBE;
        public ReportController(IReportBE reportBE,
                               IMapper mapper) : base(mapper)
        {
            ReportBE = reportBE;
        }


        [HttpGet]
        [Route("rptBM02")]
        public async Task<HttpResponseMessage> rptBM02([FromUri]BM2Req req)
        {
            try
            {
                var obj = await ReportBE.rptBm02(req);
                return this.OkResult(obj);
            }
            catch(Exception ex)
            {
                return this.ErrorResult(new Error("", ex.Message));
            }
        }


        [HttpGet]
        [Route("rptBM04")]
        public async Task<HttpResponseMessage> rptBM04([FromUri]BM4Req req)
        {
            try
            {
                var obj = await ReportBE.rptBm04(req);
                return this.OkResult(obj);
            }
            catch (Exception ex)
            {
                return this.ErrorResult(new Error("", ex.Message));
            }
        }
    }
}
