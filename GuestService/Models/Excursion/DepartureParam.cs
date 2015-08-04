namespace GuestService.Models.Excursion
{
    using GuestService;
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class DepartureParam : CategoryParam, IPartnerParam
    {
        public int? DestinationState
        {
            get
            {
                return this.ds;
            }
        }

        public int? ds { get; set; }

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

        public bool WithoutPrice
        {
            get
            {
                return (this.wp.HasValue ? this.wp.Value : !Settings.ExcursionWithPriceOnlyCatalog);
            }
        }

        public bool? wp { get; set; }
    }
}

