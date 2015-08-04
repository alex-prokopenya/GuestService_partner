namespace GuestService.Models.Excursion
{
    using GuestService.Data;
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class PriceParam : BaseApiParam, IPartnerParam
    {
        public DateTime? Date
        {
            get
            {
                return this.dt;
            }
        }

        public DateTime? dt { get; set; }

        public int? ExcursionLanguage
        {
            get
            {
                return this.l;
            }
        }

        public int? l { get; set; }

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

