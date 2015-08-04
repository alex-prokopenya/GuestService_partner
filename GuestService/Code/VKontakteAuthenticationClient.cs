namespace GuestService.Code
{
    using DotNetOpenAuth.AspNet;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Script.Serialization;

    public class VKontakteAuthenticationClient : IAuthenticationClient
    {
        public string appId;
        public string appSecret;
        private string redirectUri;

        public VKontakteAuthenticationClient(string appId, string appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        public static T DeserializeJson<T>(string input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }

        void IAuthenticationClient.RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            string appId = this.appId;
            this.redirectUri = context.Server.UrlEncode(returnUrl.ToString());
            string url = string.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&response_type=code", appId, this.redirectUri);
            HttpContext.Current.Response.Redirect(url, false);
        }

        AuthenticationResult IAuthenticationClient.VerifyAuthentication(HttpContextBase context)
        {
            try
            {
                string str = context.Request["code"];
                AccessToken token = DeserializeJson<AccessToken>(Load(string.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", new object[] { this.appId, this.appSecret, str, this.redirectUri })));
                UserData data2 = DeserializeJson<UsersData>(Load(string.Format("https://api.vk.com/method/users.get?uids={0}&fields=photo_50", token.user_id))).response.First<UserData>();
                return new AuthenticationResult(true, ((IAuthenticationClient) this).ProviderName, token.user_id, data2.first_name + " " + data2.last_name, new Dictionary<string, string>());
            }
            catch (Exception exception)
            {
                return new AuthenticationResult(exception);
            }
        }

        public static string Load(string address)
        {
            string str;
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        string IAuthenticationClient.ProviderName
        {
            get
            {
                return "vkontakte";
            }
        }

        private class AccessToken
        {
            public string access_token = null;
            public string user_id = null;
        }

        private class UserData
        {
            public string first_name = null;
            public string last_name = null;
            public string photo_50 = null;
            public string uid = null;
        }

        private class UsersData
        {
            public VKontakteAuthenticationClient.UserData[] response = null;
        }
    }
}

