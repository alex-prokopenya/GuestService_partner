namespace GuestService.Models.Excursion
{
    using GuestService.Data;
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionPickupHotelsParam : BaseApiParam, IPartnerParam
    {
        public DateTime? Date
        {
            get
            {
                return this.dt;
            }
        }

        public int[] DeparturePoints
        {
            get
            {
                return this.dp;
            }
        }

        public int[] dp { get; set; }

        public DateTime? dt { get; set; }

        public int et { get; set; }

        public int ex { get; set; }

        public int Excursion
        {
            get
            {
                return this.ex;
            }
        }

        public int ExcursionTime
        {
            get
            {
                return this.et;
            }
        }

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
    }
}

