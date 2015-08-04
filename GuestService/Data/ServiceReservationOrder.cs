namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ServiceReservationOrder
    {
        public int id { get; set; }

        public string name { get; set; }

        public ServiceReservationServicetype servicetype { get; set; }
    }
}

