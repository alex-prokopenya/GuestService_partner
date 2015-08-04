namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class BookingOrder
    {
        public BookingExcursion excursion { get; set; }

        public string orderid { get; set; }
    }
}

