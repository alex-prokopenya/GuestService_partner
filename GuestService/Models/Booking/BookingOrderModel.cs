namespace GuestService.Models.Booking
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class BookingOrderModel
    {
        public GuestService.Data.BookingOrder BookingOrder { get; set; }

        public GuestService.Data.ReservationOrder ReservationOrder { get; set; }
    }
}

