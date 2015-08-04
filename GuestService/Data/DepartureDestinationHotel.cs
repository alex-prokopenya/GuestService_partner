namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class DepartureDestinationHotel
    {
        public int id { get; set; }

        public string name { get; set; }

        public Region region { get; set; }

        public Town town { get; set; }
    }
}

