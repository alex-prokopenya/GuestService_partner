namespace GuestService
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class ViewEngineConfig
    {
        public static void Register(ViewEngineCollection viewEngineCollection)
        {
            foreach (IViewEngine engine in viewEngineCollection)
            {
                RazorViewEngine engine2 = engine as RazorViewEngine;
                if (engine2 != null)
                {
                    List<string> list = new List<string> {
                        string.Format("{0}/{{0}}.cshtml", CustomizationPath.ViewsFolder)
                    };
                    list.AddRange(engine2.ViewLocationFormats);
                    engine2.PartialViewLocationFormats = list.ToArray();
                }
            }
        }
    }
}

