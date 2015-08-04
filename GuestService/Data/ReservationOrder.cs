namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ReservationOrder
    {
        public DateTime datefrom { get; set; }

        public DateTime datetill { get; set; }

        public ExcursionReservationOrder excursion { get; set; }

        public FreightReservationOrder freight { get; set; }

        public HotelReservationOrder hotel { get; set; }

        public string id { get; set; }

        public int? localid { get; set; }

        public string note { get; set; }

        public ReservationPartner partner { get; set; }

        public ReservationPax pax { get; set; }

        public List<string> peopleids { get; set; }

        public ReservationOrderPrice price { get; set; }

        public ServiceReservationOrder service { get; set; }

        public ReservationStatus status { get; set; }

        public TransferReservationOrder transfer { get; set; }
    }
}

