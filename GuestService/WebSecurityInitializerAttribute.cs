namespace GuestService
{
    using GuestService.Code;
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public sealed class WebSecurityInitializerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            WebSecurityInitializer.Initialize();
        }
    }
}

