namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class GuestOrder
    {
        public string description { get; set; }

        public int? hotelid { get; set; }

        public DatePeriod period { get; set; }

        public int? serviceid { get; set; }

        public string title { get; set; }
    }
}

