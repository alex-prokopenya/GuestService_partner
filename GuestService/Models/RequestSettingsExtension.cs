namespace GuestService.Models
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web;

    public static class RequestSettingsExtension
    {
        public static GuestService.Models.RequestSettings RequestSettings(this HttpRequestBase request)
        {
            return new GuestService.Models.RequestSettings(request, request.RequestContext.HttpContext.Response);
        }
    }
}

