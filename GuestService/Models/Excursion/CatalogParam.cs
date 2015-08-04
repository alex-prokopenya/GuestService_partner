namespace GuestService.Models.Excursion
{
    using GuestService;
    using GuestService.Data;
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class CatalogParam : BaseApiParam, IPartnerParam
    {
        public int[] c { get; set; }

        public int[] Categories
        {
            get
            {
                return this.c;
            }
        }

        public int[] d { get; set; }

        public int[] Departures
        {
            get
            {
                return this.dp;
            }
        }

        public int[] Destinations
        {
            get
            {
                return this.d;
            }
        }

        public int? DestinationState
        {
            get
            {
                return this.ds;
            }
        }

        public int[] dp { get; set; }

        public int? ds { get; set; }

        public int[] ExcursionLanguages
        {
            get
            {
                return this.l;
            }
        }

        public DateTime? fd { get; set; }

        public DateTime? FirstDate
        {
            get
            {
                return this.fd;
            }
        }

        public int[] l { get; set; }

        public DateTime? LastDate
        {
            get
            {
                return this.ld;
            }
        }

        public DateTime? ld { get; set; }

        public TimeSpan? MaxDuration
        {
            get
            {
                return this.xd;
            }
        }

        public TimeSpan? MinDuration
        {
            get
            {
                return this.nd;
            }
        }

        public TimeSpan? nd { get; set; }

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
                return (string.IsNullOrWhiteSpace(this.s) ? null : this.s);
            }
        }

        public int? sl { get; set; }

        public string so { get; set; }

        public string SortOrder
        {
            get
            {
                return this.so;
            }
        }

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

        public bool WithoutPrice
        {
            get
            {
                return (this.wp.HasValue ? this.wp.Value : !Settings.ExcursionWithPriceOnlyCatalog);
            }
        }

        public bool? wp { get; set; }

        public TimeSpan? xd { get; set; }
    }
}

