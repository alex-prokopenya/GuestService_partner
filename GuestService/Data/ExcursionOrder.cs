namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionOrder
    {
        public ExcursionContact contact { get; set; }

        public DateTime date { get; set; }

        public GeoArea departure { get; set; }

        public ExcursionGroup group { get; set; }

        public int id { get; set; }

        public Language language { get; set; }

        public string name { get; set; }

        public string note { get; set; }

        public BookingPax pax { get; set; }

        public OrderPrice price { get; set; }

        public ExcursionTime time { get; set; }
    }
}

