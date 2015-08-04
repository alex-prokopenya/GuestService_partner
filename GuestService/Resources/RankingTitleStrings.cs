namespace GuestService.Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated]
    public class RankingTitleStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal RankingTitleStrings()
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

        public static string Rank_1
        {
            get
            {
                return ResourceManager.GetString("Rank_1", resourceCulture);
            }
        }

        public static string Rank_2
        {
            get
            {
                return ResourceManager.GetString("Rank_2", resourceCulture);
            }
        }

        public static string Rank_3
        {
            get
            {
                return ResourceManager.GetString("Rank_3", resourceCulture);
            }
        }

        public static string Rank_4
        {
            get
            {
                return ResourceManager.GetString("Rank_4", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.RankingTitleStrings", typeof(RankingTitleStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

