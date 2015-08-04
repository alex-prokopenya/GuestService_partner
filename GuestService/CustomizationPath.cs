namespace GuestService
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public static class CustomizationPath
    {
        private static string Combine(string a, string b, string c = null, string d = null, string e = null, string f = null)
        {
            if (a == null)
            {
                throw new ArgumentNullException("a");
            }
            ValidatePathPart(a);
            string str = a;
            if (b != null)
            {
                ValidatePathPart(b);
                str = Path.Combine(str, b);
                if (c == null)
                {
                    return str;
                }
                ValidatePathPart(c);
                str = Path.Combine(str, c);
                if (d == null)
                {
                    return str;
                }
                ValidatePathPart(d);
                str = Path.Combine(str, d);
                if (e == null)
                {
                    return str;
                }
                ValidatePathPart(e);
                str = Path.Combine(str, e);
                if (f != null)
                {
                    ValidatePathPart(f);
                    str = Path.Combine(str, f);
                }
            }
            return str;
        }

        private static void ValidatePathPart(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.IndexOf(Path.PathSeparator) >= 0)
            {
                throw new ArgumentException("path part contains invalid characters", "name");
            }
        }

        public static string AdoptionFolder
        {
            get
            {
                return "~/Customization/Adoption";
            }
        }

        public static string AgreementsFolder
        {
            get
            {
                return "~/Customization/Agreements";
            }
        }

        public static string ViewsFolder
        {
            get
            {
                return "~/Customization/Views";
            }
        }
    }
}

