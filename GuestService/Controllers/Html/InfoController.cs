namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Models.Welcome;
    using Sm.System.Mvc;
    using Sm.System.Mvc.Language;
    using System;
    using System.Web.Mvc;

    [HttpPreferences, WebSecurityInitializer, UrlLanguage]
    public class InfoController : BaseController
    {
        [HttpGet, ActionName("index")]
        public ActionResult Index(string id)
        {
            return base.View(id, new object { });
        }
    }
}

