namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class FreightReservationOrder
    {
        public FreightPoint arrival { get; set; }

        public FreightReservationBookingclass bookingclass { get; set; }

        public FreightPoint departure { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public FreightReservationPlace place { get; set; }
    }
}

