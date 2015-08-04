namespace GuestService.Models.Booking
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class BookingCartResult
    {
        public BookingModel Form { get; set; }

        public ReservationState Reservation { get; set; }
    }
}

