namespace GuestService.Models.Guide
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class HotelGuideParam : BaseApiParam
    {
        public int? h { get; set; }

        public int? Hotel
        {
            get
            {
                return this.h;
            }
        }

        public DateTime? pb { get; set; }

        public DateTime? pe { get; set; }

        public DateTime? PeriodBegin
        {
            get
            {
                return this.pb;
            }
        }

        public DateTime? PeriodEnd
        {
            get
            {
                return this.pe;
            }
        }
    }
}

