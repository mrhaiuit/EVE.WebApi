using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace EVE.WebApi.Authentication.Helper
{
    public class AppUtil
    {
        public static string GetClientIp(HttpRequestMessage request)
        {
            if(request != null)
            {
                if(request.Properties.ContainsKey("MS_HttpContext"))
                {
                    return ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }

                if(request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
                    return prop.Address;
                }

                if(HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
            }

            return string.Empty;
        }
    }
}
