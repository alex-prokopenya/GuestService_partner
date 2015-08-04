namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class HotelReservationOrder
    {
        public HotelReservationHtplace htplace { get; set; }

        public int id { get; set; }

        public HotelReservationMeal meal { get; set; }

        public string name { get; set; }

        public HotelReservationRoom room { get; set; }
    }
}

