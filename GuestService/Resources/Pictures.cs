namespace GuestService.Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    public class Pictures
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Pictures()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static Bitmap GuideNoPhoto
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("GuideNoPhoto", resourceCulture);
            }
        }

        public static Bitmap nophoto
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("nophoto", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.Pictures", typeof(Pictures).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

