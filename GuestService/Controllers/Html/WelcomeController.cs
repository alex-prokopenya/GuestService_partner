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
    public class WelcomeController : BaseController
    {
        public ActionResult Error()
        {
            throw new Exception("ERROR EXCEPTION");
        }

        public ActionResult HotelInfo(string id)
        {
            return base.View();
        }

        [HttpGet, ActionName("index")]
        public ActionResult Index(WelcomeParam param)
        {
            if (param != null)
            {
                HttpPreferencesManager manager = new HttpPreferencesManager(this);
                if (param.visual != null)
                {
                    if (HttpPreferences.CheckVisualTheme(param.visual))
                    {
                        HttpPreferences.Current.VisualTheme = param.visual;
                    }
                    base.Request.RemoveRouteValue("visual");
                }
                if (param.locationhotel != null)
                {
                    if (HttpPreferences.CheckLocationHotel(param.locationhotel))
                    {
                        HttpPreferences.Current.LocationHotel = param.locationhotel;
                    }
                    base.Request.RemoveRouteValue("locationhotel");
                }
                manager.Save(HttpPreferences.Current);
            }
            string[] strArray = Settings.GuestDefaultPage.Split(".,;/".ToCharArray());
            return this.RedirectToAction((strArray.Length > 1) ? strArray[1] : strArray[0], (strArray.Length > 1) ? strArray[0] : "guest", base.Request.QueryStringAsRouteValues());
        }

        [ActionName("reset"), HttpGet]
        public ActionResult Reset()
        {
            new HttpPreferencesManager(this).Save(HttpPreferences.Current);
            return base.RedirectToAction("index");
        }
    }
}

