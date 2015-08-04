namespace GuestService.Models.Excursion
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchParam : BaseApiParam
    {
        public string s { get; set; }

        public int? SearchLimit
        {
            get
            {
                return this.sl;
            }
        }

        public string SearchText
        {
            get
            {
                return this.s;
            }
        }

        public int? sl { get; set; }

        public int? sp { get; set; }

        public string spa { get; set; }

        public int? StartPoint
        {
            get
            {
                return this.sp;
            }
        }

        public string StartPointAlias
        {
            get
            {
                return this.spa;
            }
        }
    }
}

