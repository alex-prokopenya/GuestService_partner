namespace GuestService
{
    using DotNetOpenAuth.AspNet;
    using GuestService.Code;
    using Microsoft.Web.WebPages.OAuth;
    using Sm.System.SettingsExtension;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            string str = ConfigurationManager.AppSettings.AsString("oauthMicrosoftClientId", null);
            string str2 = ConfigurationManager.AppSettings.AsString("oauthMicrosoftClientSecret", null);
            if (!(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)))
            {
                OAuthWebSecurity.RegisterMicrosoftClient(str, str2);
            }
            str = ConfigurationManager.AppSettings.AsString("oauthTwitterConsumerKey", null);
            str2 = ConfigurationManager.AppSettings.AsString("oauthTwitterConsumerSecret", null);
            if (!(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)))
            {
                OAuthWebSecurity.RegisterTwitterClient(str, str2);
            }
            str = ConfigurationManager.AppSettings.AsString("oauthFacebookAppId", null);
            str2 = ConfigurationManager.AppSettings.AsString("oauthFacebookAppSecret", null);
            if (!(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)))
            {
                OAuthWebSecurity.RegisterFacebookClient(str, str2);
            }
            if (ConfigurationManager.AppSettings.AsBool("oauthGoogle", false))
            {
                OAuthWebSecurity.RegisterGoogleClient();
            }
            str = ConfigurationManager.AppSettings.AsString("oauthVKontakteAppId", null);
            str2 = ConfigurationManager.AppSettings.AsString("oauthVKontakteAppSecret", null);
            if (!(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)))
            {
                IAuthenticationClient client = new VKontakteAuthenticationClient(str, str2);
                string displayName = "VK";
                IDictionary<string, object> extraData = null;
                OAuthWebSecurity.RegisterClient(client, displayName, extraData);
            }
        }
    }
}

