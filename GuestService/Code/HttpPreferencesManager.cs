namespace GuestService.Code
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class HttpPreferencesManager : IDisposable
    {
        private Controller controller;
        private const string cookieName = "preferences";
        private const string locationHotelName = "lh";
        private const string visualThemeName = "vt";

        public HttpPreferencesManager(Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException();
            }
            this.controller = controller;
        }

        public void Dispose()
        {
            this.Save(HttpPreferences.Current);
        }

        public HttpPreferences LoadPreferences()
        {
            HttpCookie cookie = this.controller.HttpContext.Request.Cookies["preferences"];
            if ((cookie != null) && (cookie.Values != null))
            {
                HttpPreferences preferences = HttpPreferences.CreateDefault();
                string str = cookie.Values["vt"];
                if ((str != null) && HttpPreferences.CheckVisualTheme(str))
                {
                    preferences.VisualTheme = str;
                }
                str = cookie.Values["lh"];
                if ((str != null) && HttpPreferences.CheckLocationHotel(str))
                {
                    preferences.LocationHotel = string.IsNullOrEmpty(str) ? null : str;
                }
                return preferences;
            }
            return null;
        }

        public void Save(HttpPreferences preferences)
        {
            if (preferences == null)
            {
                throw new ArgumentNullException("preferences");
            }
            HttpCookie cookie = new HttpCookie("preferences") {
                Path = this.controller.Url.Content("~"),
                Expires = DateTime.Now.AddYears(10)
            };
            cookie.Values["vt"] = preferences.VisualTheme;
            cookie.Values["lh"] = preferences.LocationHotel;
            this.controller.HttpContext.Response.Cookies.Set(cookie);
        }
    }
}

