namespace GuestService.Models.Excursion
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionIndexWebParam : IPartnerParam
    {
        public int[] c { get; set; }

        public int[] Categories
        {
            get
            {
                return this.c;
            }
        }

        public string cid { get; set; }

        public int[] d { get; set; }

        public DateTime? Date
        {
            get
            {
                return this.dt;
            }
        }

        public int[] Destinations
        {
            get
            {
                return this.d;
            }
        }

        public DateTime? dt { get; set; }

        public int? ex { get; set; }

        public int? Excursion
        {
            get
            {
                return this.ex;
            }
        }

        public int[] ExcursionLanguages
        {
            get
            {
                return this.l;
            }
        }

        public string ExternalCartId
        {
            get
            {
                return this.cid;
            }
        }

        public int[] l { get; set; }

        public string pa { get; set; }

        public string PartnerAlias
        {
            get
            {
                return this.pa;
            }
        }

        public string PartnerSessionID
        {
            get
            {
                return this.psid;
            }
        }

        public string psid { get; set; }

        public string s { get; set; }

        public string sc { get; set; }

        public string SearchText
        {
            get
            {
                return (string.IsNullOrWhiteSpace(this.s) ? null : this.s);
            }
        }

        public string ShowCommand
        {
            get
            {
                return this.sc;
            }
        }

        public string spa { get; set; }

        public string StartPointAlias
        {
            get
            {
                return this.spa;
            }
        }

        public string visualtheme { get; set; }
    }
}

