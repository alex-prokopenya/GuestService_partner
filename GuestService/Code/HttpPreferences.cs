namespace GuestService.Code
{
    using Sm.System.Extension;
    using System;
    using System.Configuration;
    using System.Linq;

    public class HttpPreferences
    {
        [ThreadStatic]
        private static HttpPreferences current;
        private string locationHotel;
        private string visualTheme;

        public static bool CheckLocationHotel(string value)
        {
            return true;
        }

        public static bool CheckVisualTheme(string value)
        {
            bool result;
            if (string.IsNullOrEmpty(value))
            {
                result = false;
            }
            else
            {
                string[] themes = (
                    from m in ConfigurationManager.AppSettings.AsString("visualThemeSupported", "").Split(";,".ToCharArray())
                    select m.ToLower()).ToArray<string>();
                result = themes.Contains(value);
            }
            return result;
        }

        public static HttpPreferences CreateDefault()
        {
            return new HttpPreferences { VisualTheme = ConfigurationManager.AppSettings.AsString("visualThemeDefault", "").ToLower(), LocationHotel = null };
        }

        public static HttpPreferences Current
        {
            get
            {
                if (current == null)
                {
                    current = CreateDefault();
                }
                return current;
            }
            set
            {
                current = value;
            }
        }

        public string LocationHotel
        {
            get
            {
                return this.locationHotel;
            }
            set
            {
                this.locationHotel = value;
            }
        }

        public string VisualTheme
        {
            get
            {
                return this.visualTheme;
            }
            set
            {
                if (!(string.IsNullOrEmpty(value) || CheckVisualTheme(value)))
                {
                    throw new ArgumentException(string.Format("invalid visual theme '{0}'", value));
                }
                this.visualTheme = value;
            }
        }
    }
}

