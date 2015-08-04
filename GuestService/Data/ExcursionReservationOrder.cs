namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionReservationOrder
    {
        public ExcursionReservationGroup grouptype { get; set; }

        public int id { get; set; }

        public ExcursionReservationLanguage language { get; set; }

        public string name { get; set; }

        public PickupPlace pickuphotel { get; set; }

        public PickupPlace pickuppoint { get; set; }

        public ExcursionReservationTime time { get; set; }
    }
}

