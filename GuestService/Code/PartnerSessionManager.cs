namespace GuestService.Code
{
    using GuestService.Data;
    using System;
    using System.Web;
    using System.Web.Mvc;

    public static class PartnerSessionManager
    {
        private const string PartnerSessionID = "partnerSessionID";

        public static void CheckPartnerSession(Controller controller, IPartnerParam param)
        {
            HttpCookie cookie;
            if (param.PartnerSessionID != null)
            {
                cookie = new HttpCookie("partnerSessionID") {
                    Path = controller.Url.Content("~"),
                    Expires = DateTime.Now.AddDays(1.0),
                    Value = param.PartnerSessionID
                };
                controller.HttpContext.Response.Cookies.Set(cookie);
            }
            else if (controller.HttpContext.Request["psid"] != null)
            {
                cookie = new HttpCookie("partnerSessionID") {
                    Path = controller.Url.Content("~"),
                    Expires = DateTime.Now.AddDays(-1.0)
                };
                controller.HttpContext.Response.Cookies.Set(cookie);
            }
            else if (param.PartnerAlias == null)
            {
                cookie = controller.HttpContext.Request.Cookies["partnerSessionID"];
                if (cookie != null)
                {
                    param.psid = cookie.Value;
                }
            }
        }
    }
}

