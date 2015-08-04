namespace GuestService.Code
{
    using System;
    using System.Resources;
    using System.Runtime.CompilerServices;

    public static class ResourceManagerExtension
    {
        public static string Get(this ResourceManager manager, string name)
        {
            return manager.GetString(name);
        }

        public static string Get(this ResourceManager manager, string name, params object[] param)
        {
            return string.Format(manager.GetString(name), param);
        }
    }
}

