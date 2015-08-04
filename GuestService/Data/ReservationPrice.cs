namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReservationPrice
    {
        public string currency { get; set; }

        public decimal topay { get; set; }

        public decimal total { get; set; }
    }
}

