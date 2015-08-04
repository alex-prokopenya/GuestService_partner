namespace GuestService.Code
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=false)]
    public class HttpPreferencesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                HttpPreferences preferences = new HttpPreferencesManager(controller).LoadPreferences();
                if (preferences != null)
                {
                    HttpPreferences.Current = preferences;
                }
            }
        }
    }
}

