namespace GuestService.Models.Guest
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class DepartureParam : BaseApiParam
    {
        public int? c { get; set; }

        public int? Claim
        {
            get
            {
                return this.c;
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

        public int? h { get; set; }

        public string ha { get; set; }

        public int? Hotel
        {
            get
            {
                return this.h;
            }
        }

        public string HotelAlias
        {
            get
            {
                return this.ha;
            }
        }

        public DateTime? LastDate
        {
            get
            {
                return this.ld;
            }
        }

        public DateTime? ld { get; set; }
    }
}

