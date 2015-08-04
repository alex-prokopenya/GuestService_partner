namespace GuestService.Code
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;

    public static class HttpRequestBaseExtension
    {
        public static Uri BaseServerAddress(this HttpRequestBase request)
        {
            string str = request.ServerVariables["HTTP_X_FORWARDED_HOST"];
            return new Uri(request.Url.Scheme + "://" + (!string.IsNullOrEmpty(str) ? str : request.Url.Authority));
        }

        public static string DumpValues(this HttpRequestBase request)
        {
            StringBuilder builder = new StringBuilder();
            if (request != null)
            {
                if ((request.QueryString != null) && (request.QueryString.Keys.Count > 0))
                {
                    builder.AppendLine("QueryString:");
                    foreach (string str in request.QueryString.Keys)
                    {
                        builder.AppendLine(string.Format(" {0} = '{1}'", str, request.QueryString[str]));
                    }
                }
                if ((request.Params != null) && (request.Params.Keys.Count > 0))
                {
                    builder.AppendLine("Params:");
                    foreach (string str in request.Params.Keys)
                    {
                        builder.AppendLine(string.Format(" {0} = '{1}'", str, request.Params[str]));
                    }
                }
                if ((request.Form != null) && (request.Form.Keys.Count > 0))
                {
                    builder.AppendLine("Form:");
                    foreach (string str in request.Form.Keys)
                    {
                        builder.AppendLine(string.Format(" {0} = '{1}'", str, request.Form[str]));
                    }
                }
                if ((request.ServerVariables != null) && (request.ServerVariables.Keys.Count > 0))
                {
                    builder.AppendLine("ServerVariables:");
                    foreach (string str in request.ServerVariables.Keys)
                    {
                        builder.AppendLine(string.Format(" {0} = '{1}'", str, request.ServerVariables[str]));
                    }
                }
            }
            return builder.ToString();
        }
    }
}

